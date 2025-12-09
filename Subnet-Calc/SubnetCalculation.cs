using System.Collections.Generic;
using System.Net;
using IPv4_Calculator.BL;

namespace Subnet_Calc;

public class SubnetCalculation
{
	public class SubnetInfo
	{
		public int Prefix { get; set; }                        // Suffix / Präfix
		public IPAddress Mask { get; set; } = IPAddress.Any;   // Maske
		public IPAddress Netz { get; set; } = IPAddress.Any;   // Netzadresse
		public IPAddress Broadcast { get; set; } = IPAddress.Any;   // Broadcast
		public IPAddress ErsterHost { get; set; } = IPAddress.Any;  // erster Host
		public IPAddress LetzterHost { get; set; } = IPAddress.Any; // letzter Host
	}

	public static SubnetInfo CalculateFromIpAndSuffix(IPAddress ip, int suffix)
	{
		uint ipInt = Calculation.IPToInt(ip);
		uint mask = Calculation.SuffixToMask(suffix);      // Maske
		uint netz = ipInt & mask;                          // Netz
		uint broadcast = netz | ~mask;                     // Broadcast
		uint ersterHost = netz + 1;                        // erster Host
		uint letzterHost = broadcast - 1;                  // letzter Host

		return new SubnetInfo
		{
			Prefix = suffix,
			Mask = Calculation.IntToIP(mask),
			Netz = Calculation.IntToIP(netz),
			Broadcast = Calculation.IntToIP(broadcast),
			ErsterHost = Calculation.IntToIP(ersterHost),
			LetzterHost = Calculation.IntToIP(letzterHost)
		};
	}

	public static SubnetInfo CalculateFromIpAndSuffix(string IPv4WithCIDRText, string CIDRWithIPv4Text)
	{
		IPAddress ip = IPAddress.Parse(IPv4WithCIDRText);
		int suffix = int.Parse(CIDRWithIPv4Text);
		return CalculateFromIpAndSuffix(ip, suffix);
	}

	public static SubnetInfo CalculateFromIpAndMask(IPAddress ip, IPAddress maskIp)
	{
		uint mask = Calculation.IPToInt(maskIp);
		int prefix = MaskToPrefix(mask);
		return CalculateFromIpAndSuffix(ip, prefix);
	}

	public static SubnetInfo CalculateFromIpAndMask(string IPv4WithSubnetText, string SubnetWithIPv4Text)
	{
		IPAddress ip = IPAddress.Parse(IPv4WithSubnetText);
		IPAddress maskIp = IPAddress.Parse(SubnetWithIPv4Text);
		return CalculateFromIpAndMask(ip, maskIp);
	}

	public static List<SubnetInfo> GenerateSubnetInfos(IPAddress networkOrIp, int currentSuffix, int newSuffix)
	{
		var list = new List<SubnetInfo>();
		if (newSuffix <= currentSuffix || newSuffix > 32) return list;

		uint baseNet = Calculation.IPToInt(networkOrIp) & Calculation.SuffixToMask(currentSuffix);
		int diff = newSuffix - currentSuffix;
		uint subnetCount = 1u << diff;
		uint step = 1u << (32 - newSuffix);

		for (uint i = 0; i < subnetCount; i++)
		{
			uint netInt = baseNet + i * step;
			var info = CalculateFromIpAndSuffix(Calculation.IntToIP(netInt), newSuffix);
			list.Add(info);
		}

		return list;
	}

	public static List<SubnetInfo> GenerateSubnetInfos(string IPv4WithCIDRText, string CIDRWithIPv4Text, int newSuffix)
	{
		IPAddress ip = IPAddress.Parse(IPv4WithCIDRText);
		int currentSuffix = int.Parse(CIDRWithIPv4Text);
		return GenerateSubnetInfos(ip, currentSuffix, newSuffix);
	}

	private static int MaskToPrefix(uint mask)
	{
		int count = 0;
		uint m = mask;
		while (m != 0)
		{
			m &= (m - 1);
			count++;
		}
		return count;
	}
}
