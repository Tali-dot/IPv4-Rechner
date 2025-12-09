using System.Net;

namespace IPv4_Calculator.BL;

public class Calculation
{
    // 0xFFFFFFFF -> 32 Einsen in Binär
    //Schiebt nach links und macht hinten nullen
    public static uint SuffixToMask(int suffix)
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
