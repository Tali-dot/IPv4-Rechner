using Subnet_Calc;
using System;

namespace IPv4_Calculator;

public static class ErrorMessages
{

	public static bool ErrorMessage13 (List<SubnetCalculation.SubnetInfo> subnets)
	{
		if (subnets.Count < 3)
		{
			Console.BackgroundColor = ConsoleColor.Red;
			Console.ForegroundColor = ConsoleColor.DarkRed;
			Console.WriteLine();
			Console.WriteLine("FEHLERCODE: 13");
			Console.WriteLine("Es konnten nicht mindestens 3 Subnetze erzeugt werden.");
			Console.ResetColor();
			return true;
		}
		return false; 
	}
	public static bool ErrorMessage12 (int newPrefix, int currentPrefix)
	{
		if (newPrefix > 32)
		{
			Console.BackgroundColor = ConsoleColor.Red;
			Console.ForegroundColor = ConsoleColor.DarkRed;
			Console.WriteLine();
			Console.WriteLine("FEHLERCODE: 12");
			Console.WriteLine("Das angegebene Präfix ist zu groß, um es noch in mindestens 3 Subnetze aufzuteilen.");
			Console.WriteLine($"Aktuelles Präfix: /{currentPrefix}, benötigtes neues Präfix: /{newPrefix} (ungültig).");
			Console.ResetColor();
			return true;
		}
		return false;
	}

	public static bool ErrorMessage11(int newPrefix, int currentPrefix)
	{
		if (newPrefix > 32)
		{
			Console.BackgroundColor = ConsoleColor.Red;
			Console.ForegroundColor = ConsoleColor.DarkRed;
			Console.WriteLine();
			Console.WriteLine("FEHLERCODE: 11");
			Console.WriteLine("Die angegebene Subnetzmaske erlaubt keine weitere Aufteilung in mindestens 3 Subnetze.");
			Console.WriteLine($"Aktuelles Präfix (aus Maske): /{currentPrefix}, benötigtes neues Präfix: /{newPrefix} (ungültig).");
			Console.ResetColor();
			return true;
		}
		return false;
	}

	public static bool ErrorMessage10(List<SubnetCalculation.SubnetInfo>?subnets)
	{
		if (subnets.Count < 3)
		{
			Console.BackgroundColor = ConsoleColor.Red;
			Console.ForegroundColor = ConsoleColor.DarkRed;
			Console.WriteLine();
			Console.WriteLine("FEHLERCODE: 10");
			Console.WriteLine("Es konnten nicht mindestens 3 Subnetze erzeugt werden.");
			Console.ResetColor();
			return true; 
		}
		return false;
	}

	public static bool ErrorMessage09(string IPv4WithCIDRText)
	{
		if(IPv4WithCIDRText != null && string.IsNullOrWhiteSpace(IPv4WithCIDRText))
		{
			Console.BackgroundColor = ConsoleColor.Red;
			Console.ForegroundColor = ConsoleColor.DarkRed;
			Console.WriteLine();
			Console.WriteLine("FEHLERCODE: 09");
			Console.WriteLine("Das Feld darf nicht leer sein. Bitte wählen Sie eine gültige IPv4 Adresse!");
			Console.ResetColor();
			return true;
		}
		return false; 
	}
	public static bool ErrorMessage08(string CIDRWithIPv4Text)
	{
		if(CIDRWithIPv4Text != null && string.IsNullOrWhiteSpace(CIDRWithIPv4Text))
		{
			Console.BackgroundColor = ConsoleColor.Red;
			Console.ForegroundColor = ConsoleColor.DarkRed;
			Console.WriteLine();
			Console.WriteLine("FEHLERCODE: 08");
			Console.WriteLine("Das Feld darf nicht leer sein. Bitte wählen Sie ein gültiges Präfix!");
			Console.ResetColor();
			return true;
		}
		return false; 
	}

	public static bool ErrorMessage07(string IPv4WithSubnetText)
	{
		if(IPv4WithSubnetText != null && string.IsNullOrWhiteSpace(IPv4WithSubnetText))
		{
			Console.BackgroundColor = ConsoleColor.Red;
			Console.ForegroundColor = ConsoleColor.DarkRed;
			Console.WriteLine();
			Console.WriteLine("FEHLERCODE: 07");
			Console.WriteLine("Das Feld darf nicht leer sein. Bitte wählen Sie eine gültige IPv4 Adresse!");
			Console.ResetColor();
			return true;
		}
		return false;
	}

	public static bool ErrorMessage06(string SubnetWithIPv4Text)
	{
		if(SubnetWithIPv4Text != null && string.IsNullOrWhiteSpace(SubnetWithIPv4Text))
		{
			Console.BackgroundColor = ConsoleColor.Red;
			Console.ForegroundColor = ConsoleColor.DarkRed;
			Console.WriteLine();
			Console.WriteLine("FEHLERCODE: 06");
			Console.WriteLine("Das Feld darf nicht leer sein. Bitte wählen Sie eine gültige Subnetzmaske!");
			Console.ResetColor();
			return true;
		}
		return false;
	}

	public static bool ErrorMessage05(string SubnetOption)
	{
		if (SubnetOption != null && string.IsNullOrWhiteSpace(SubnetOption))
		{
			Console.BackgroundColor = ConsoleColor.Red;
			Console.ForegroundColor = ConsoleColor.DarkRed;
			Console.WriteLine();
			Console.WriteLine("FEHLERCODE: 05");
			Console.WriteLine("Das Feld darf nicht leer sein. Bitte wählen Sie 1 oder 2!");
			Console.ResetColor();
			return true;
		}

		return false;
	}
	public static bool ErrorMessage04(string suffixtext)
	{
		if (string.IsNullOrWhiteSpace(suffixtext))
		{
			Console.BackgroundColor = ConsoleColor.Red;
			Console.ForegroundColor = ConsoleColor.DarkRed;
			Console.WriteLine();
			Console.WriteLine("FEHLERCODE: 04");
			Console.WriteLine("Das Feld Suffix darf nicht leer sein. Bitte geben Sie einen gültigen Wert an!");
			Console.ResetColor();
			return true;
		}
		return false;
	}

	public static bool ErrorMessage03(string ipText)
	{
		if (string.IsNullOrWhiteSpace(ipText))
		{
			Console.BackgroundColor = ConsoleColor.Red;
			Console.ForegroundColor = ConsoleColor.DarkRed;
			Console.WriteLine();
			Console.WriteLine("FEHLERCODE: 03");
			Console.WriteLine("Das Feld IP-Adresse darf nicht leer sein. Bitte geben Sie einen gültigen Wert an!");
			Console.ResetColor();
			return true;
		}
		return false;
	}

	public static bool ErrorMessage02(string option)
	{
		if (option != null && string.IsNullOrWhiteSpace(option))
		{
			Console.BackgroundColor = ConsoleColor.Red;
			Console.ForegroundColor = ConsoleColor.DarkRed;
			Console.WriteLine();
			Console.WriteLine("FEHLERCODE: 02");
			Console.WriteLine("Das Feld darf nicht leer sein. Bitte wählen Sie 1 oder 2!");
			Console.ResetColor();
			return true;
		}
		return false;
	}

	public static bool ErrorMessage01(string option)
	{
		if (option != null && option != "1" && option != "2")
		{
			Console.BackgroundColor = ConsoleColor.Red;
			Console.ForegroundColor = ConsoleColor.DarkRed;
			Console.WriteLine();
			Console.WriteLine("FEHLERCODE: 01");
			Console.WriteLine("Ungültige Eingabe. Bitte wählen Sie 1 oder 2!");
			Console.ResetColor();
			return true;
		}
		return false;
	}

	public static bool ErrorMessage00(string SubnetOption)
	{
		if (SubnetOption != null && SubnetOption != "1" && SubnetOption != "2")
		{
			Console.BackgroundColor = ConsoleColor.Red;
			Console.ForegroundColor = ConsoleColor.DarkRed;
			Console.WriteLine();
			Console.WriteLine("FEHLERCODE: 00");
			Console.WriteLine("Ungültige Eingabe. Bitte wählen Sie 1 oder 2!");
			Console.ResetColor();
			return true;
		}
		return false;
	}
}