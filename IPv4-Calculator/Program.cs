using System;
using System.Net;
using IPv4_Calculator;
using IPv4_Calculator.BL;
using Subnet_Calc;

public class Program : Calculation
{
	public static void Main()
	{
		// Startmenü
		Console.BackgroundColor = ConsoleColor.Black;
		Console.ForegroundColor = ConsoleColor.White;
		Console.WriteLine("Was möchten sie berechnen?");
		Console.WriteLine();
		Console.WriteLine("Verfügbare Optionen:");
		Console.WriteLine();
		Console.WriteLine("   1: IPv4 Rechner");
		Console.WriteLine();
		Console.WriteLine("      Normaler IP Rechner der aus IP Adresse und Präfix die Werte:");
		Console.WriteLine("      Maske, Netzadresse, Broadcast, erster Host und letzter Host ausrechnet.");
		Console.WriteLine();
		Console.WriteLine("   2: IPv4 Subnetz Rechner");
		Console.WriteLine();
		Console.WriteLine("      Rechner der aus IPv4 Adresse und Präfix/Subnetzmaske 3 zusätzliche Subnetze bildet.");
		Console.WriteLine();
		string Optionstext = Console.ReadLine();
		string option = Optionstext?.Trim() ?? string.Empty;

		// Klassischer IPv4 Rechner
		if (option == "1")
		{
			Console.WriteLine();
			Console.WriteLine("Bitte geben sie die IP-Adresse und den Suffix an:");
			Console.WriteLine();
			Console.Write("IP-Adresse: ");
			string ipText = Console.ReadLine();

			if (ErrorMessages.ErrorMessage03(ipText))
			{
				return;
			}

			Console.Write("Suffix: ");
			string suffixtext = Console.ReadLine();

			if (ErrorMessages.ErrorMessage04(suffixtext))
			{
				return;
			}

			int suffix = int.Parse(suffixtext);

			// Text wird in IP-Objekt umgewandelt
			IPAddress ip = IPAddress.Parse(ipText);
			// in 32-bit Zahl
			uint ipInt = IPToInt(ip);

			// Maske wird aus Suffix gebaut
			uint mask = SuffixToMask(suffix);
			// Netz = IP-Adresse log. UND Maske 
			uint netz = ipInt & mask;
			// broadcast = Netz OR invertierte Maske
			uint broadcast = netz | ~mask;
			// ersterHost = Netz + 1
			uint ersterHost = netz + 1;
			// letzterHost = Broadcast - 1
			uint letzterHost = broadcast - 1;

			// Ausgabe der Werte
			Console.WriteLine();
			Console.BackgroundColor = ConsoleColor.DarkGreen;
			Console.ForegroundColor = ConsoleColor.Gray;
			Console.WriteLine("Berechnete Werte:");
			Console.WriteLine();
			Console.WriteLine("Maske:");
			Console.WriteLine(IntToIP(mask));

			Console.WriteLine();
			Console.WriteLine("Netzadresse:");
			Console.WriteLine(IntToIP(netz));

			Console.WriteLine();
			Console.WriteLine("Broadcast:");
			Console.WriteLine(IntToIP(broadcast));

			Console.WriteLine();
			Console.WriteLine("erster Host:");
			Console.WriteLine(IntToIP(ersterHost));

			Console.WriteLine();
			Console.WriteLine("letzter Host:");
			Console.WriteLine(IntToIP(letzterHost));
			Console.ResetColor();
		}
		// IPv4 Subnetz Rechner
		else if (option == "2")
		{
			Console.WriteLine();
			Console.WriteLine("Unterstützte Werte:");
			Console.WriteLine();
			Console.WriteLine("   1: IPv4 Adresse mit Präfix");
			Console.WriteLine("   2: IPv4 Adresse mit Subnetzmaske");
			Console.WriteLine();
			Console.WriteLine("Bitte wählen sie ihre Option:");
			Console.WriteLine();
			string SubnetOptionText = Console.ReadLine();
			string SubnetOption = SubnetOptionText?.Trim() ?? string.Empty;

			// IPv4 mit Präfix Menü: Netz in mindestens 3 Subnetze aufteilen
			if (SubnetOption == "1")
			{
				Console.WriteLine();
				Console.WriteLine("Bitte geben sie ihre IPv4 Adresse ein:");
				Console.WriteLine();
				string IPv4WithCIDRText = Console.ReadLine();

				if (ErrorMessages.ErrorMessage09(IPv4WithCIDRText))
				{
					return;
				}

				Console.WriteLine();
				Console.WriteLine("Bitte geben sie ihr Suffix an (z.B. 24):");
				Console.WriteLine();
				string CIDRWithIPv4Text = Console.ReadLine();

				if (ErrorMessages.ErrorMessage08(CIDRWithIPv4Text))
				{
					return;
				}

				int currentPrefix = int.Parse(CIDRWithIPv4Text);

				// Wir brauchen mindestens 3 Subnetze -> diff = 2 -> neues Präfix = currentPrefix + 2
				int newPrefix = currentPrefix + 2;

				if (ErrorMessages.ErrorMessage12(newPrefix, currentPrefix))
				{
					return;
				}

				// Subnetze generieren ( IP + Präfix)
				var subnets = SubnetCalculation.GenerateSubnetInfos(IPv4WithCIDRText, CIDRWithIPv4Text, newPrefix);

				if (ErrorMessages.ErrorMessage13(subnets))
				{
					return;
				}

				// Ausgabe der Subnetze
				Console.WriteLine();
				Console.BackgroundColor = ConsoleColor.DarkGreen;
				Console.ForegroundColor = ConsoleColor.Gray;
				Console.WriteLine($"Ausgangsnetz: {IPv4WithCIDRText}/{currentPrefix}");
				Console.WriteLine($"Aufgeteilt in {subnets.Count} Subnetze mit Präfix /{newPrefix}:");
				Console.WriteLine();

				int index = 1;
				foreach (var info in subnets)
				{
					Console.WriteLine($"Subnetz {index}:");
					Console.WriteLine($"  Netzadresse : {info.Netz}");
					Console.WriteLine($"  Präfix      : /{info.Prefix}");
					Console.WriteLine($"  Maske       : {info.Mask}");
					Console.WriteLine($"  Broadcast   : {info.Broadcast}");
					Console.WriteLine($"  Erster Host : {info.ErsterHost}");
					Console.WriteLine($"  Letzter Host: {info.LetzterHost}");
					Console.WriteLine();
					index++;
				}

				Console.ResetColor();
			}
			// IPv4 mit Subnetzmaske Menü: GLEICHE Logik → Netz in mindestens 3 Subnetze aufteilen
			else if (SubnetOption == "2")
			{
				Console.WriteLine();
				Console.WriteLine("Bitte geben sie ihre IPv4 Adresse ein:");
				Console.WriteLine();
				string IPv4WithSubnetText = Console.ReadLine();

				if (ErrorMessages.ErrorMessage07(IPv4WithSubnetText))
				{
					return;
				}

				Console.WriteLine();
				Console.WriteLine("Bitte geben sie ihre Subnetzmaske ein:");
				Console.WriteLine();
				string SubnetWithIPv4Text = Console.ReadLine();

				if (ErrorMessages.ErrorMessage06(SubnetWithIPv4Text))
				{
					return;
				}

				// Basis-Infos aus IP + Maske holen (inkl. berechnetem Präfix)
				SubnetCalculation.SubnetInfo baseInfo =
					SubnetCalculation.CalculateFromIpAndMask(IPv4WithSubnetText, SubnetWithIPv4Text);

				int currentPrefix = baseInfo.Prefix;
				int newPrefix = currentPrefix + 2; // mindestens 3 Subnetze


				if(ErrorMessages.ErrorMessage11(newPrefix, currentPrefix))
				{
					return;
				}
				
				// Subnetze generieren:
				// baseInfo.Netz ist die Netzadresse, currentPrefix das aktuelle Präfix
				var subnets = SubnetCalculation.GenerateSubnetInfos(baseInfo.Netz, currentPrefix, newPrefix);

				if (ErrorMessages.ErrorMessage10(subnets))
				{
					return;
				}

				// Ausgabe der Subnetze
				Console.WriteLine();
				Console.BackgroundColor = ConsoleColor.DarkGreen;
				Console.ForegroundColor = ConsoleColor.Gray;
				Console.WriteLine($"Ausgangsnetz: {baseInfo.Netz}/{currentPrefix} (ermittelt aus IP + Maske)");
				Console.WriteLine($"Aufgeteilt in {subnets.Count} Subnetze mit Präfix /{newPrefix}:");
				Console.WriteLine();

				int index = 1;
				foreach (var info in subnets)
				{
					Console.WriteLine($"Subnetz {index}:");
					Console.WriteLine($"  Netzadresse : {info.Netz}");
					Console.WriteLine($"  Präfix      : /{info.Prefix}");
					Console.WriteLine($"  Maske       : {info.Mask}");
					Console.WriteLine($"  Broadcast   : {info.Broadcast}");
					Console.WriteLine($"  Erster Host : {info.ErsterHost}");
					Console.WriteLine($"  Letzter Host: {info.LetzterHost}");
					Console.WriteLine();
					index++;
				}

				Console.ResetColor();
			}

			// Fehlerbehandlung für die Auswahl im Subnet-Menü
			if (ErrorMessages.ErrorMessage00(SubnetOption))
			{
				return;
			}

			if (ErrorMessages.ErrorMessage05(SubnetOption))
			{
				return;
			}
		}

		// Fehlerbehandlung für die Hauptmenü-Option
		if (ErrorMessages.ErrorMessage01(option))
		{
			return;
		}

		if (ErrorMessages.ErrorMessage02(option))
		{
			return;
		}
	}
}
