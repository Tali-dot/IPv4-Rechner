
using System.Net;

public class Program
{
	static void Main()
	{
		Console.Write("IP-Adresse: ");
		string ipText = Console.ReadLine();

		Console.WriteLine("Suffix: ");
		int suffix = int.Parse(Console.ReadLine());

		//Text wird in IP-Objekt umgewandelt
		IPAddress ip = IPAddress.Parse(ipText);
		// in 32-bit Zahl
		uint ipInt = IPToInt(ip);

		//Maske wird aus Suffix gebaut
		uint mask = SuffixToMask(suffix);
		//Netz = IP-Adresse log. Und Mask 
		uint netz = ipInt & mask;
		//broadcast = Netz OR invertierte Maske
		uint broadcast = netz | ~mask;
		//ersterHost = Netz (bspw. 192.168.123.0) + 1 = 192.168.123.1
		uint ersterHost = netz +1;
		//letzterHost = Broadcast (bspw. 192.168.123.255) -1 = 192.168.123.254
		uint letzterHost = broadcast - 1;

		//Ausgabe der Werte
		Console.WriteLine();
		Console.WriteLine("Maske: ");
		//IntToIP wird benötigt damit der Wert als IP-Adresse und nicht als Zahl ausgegeben wird
		Console.WriteLine(IntToIP(mask));

		Console.WriteLine();
		Console.WriteLine("Netzadresse: ");
		Console.WriteLine(IntToIP(netz));

		Console.WriteLine();
		Console.WriteLine("Broadcast: ");
		Console.WriteLine(IntToIP(broadcast));

		Console.WriteLine();
		Console.WriteLine("erster Host: ");
		Console.WriteLine(IntToIP(ersterHost));

		Console.WriteLine();
		Console.WriteLine("letzter Host:");
		Console.WriteLine(IntToIP(letzterHost));
	}


	// 0xFFFFFFFF -> 32 Einsen in Binär
	//Schiebt nach links und macht hinten nullen
    static uint SuffixToMask(int suffix)
    {
		if (suffix == 0)
		{
			return 0;
		}
		//verschiebe die Einsen so weit nach links,
		//dass suffix Einsen übrig bleiben
		return 0xFFFFFFFFu << (32 - suffix);
    }

	//IP in 4 bytes umwandeln
	//drehen wegen Endian
	//Bytes -> 32 bit zahl
    public static uint IPToInt(IPAddress ip)
    {
		byte[] b = ip.GetAddressBytes();
		Array.Reverse(b);
		//BitConverter verlang LSB am anfang deshalb Reverse
		return BitConverter.ToUInt32(b);
    }

	//32-bit zahl in IP format umwandeln
	//wieder Reverse wegen Endian
	//neue IP daraus Bauen
	public static IPAddress IntToIP(uint x)
	{
		//Nach dem letzten BitConverter wieder Reverse um richtige Zahl zu bekommen
		byte[] b = BitConverter.GetBytes(x);
		Array.Reverse(b);
		//Gibt die Korrekt gebaute neue IP wieder aus
		return new IPAddress(b);
	}
}
