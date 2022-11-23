using System.Diagnostics;
using SoulsFormats;

namespace TextureToTpf;

public class Options
{
    public DCX.Type Compression;

    public Options()
    {
        Console.WriteLine("Compression Types:");
        Console.WriteLine("0: None (Will be .tpf not .tpf.dcx)");
        Console.WriteLine("1: Dark Souls 1 & 2");
        Console.WriteLine("2: Dark Souls 3");
        Console.WriteLine("3: Sekiro & Elden Ring (Requires oo2core_6_win64.dll)");
        Console.Write("Enter the number of the compression to use: ");
        
        ConsoleKeyInfo keyChoice = Console.ReadKey();
        Console.WriteLine();
        switch (keyChoice.KeyChar)
        {
            case '0':
                Compression = DCX.Type.None;
                break;
            case '1':
                Compression = DCX.Type.DCX_DFLT_10000_24_9;
                break;
            case '2':
                Compression = DCX.Type.DCX_DFLT_10000_44_9;
                break;
            case '3':
                Compression = DCX.Type.DCX_KRAK;
                break;
            default:
                throw new IOException("Not a valid compression type.");
        }
    }
}