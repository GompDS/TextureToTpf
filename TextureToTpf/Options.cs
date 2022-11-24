using SoulsFormats;

namespace TextureToTpf;

public class Options
{
    public DCX.Type Compression;

    public bool UseDcx;

    public Formats Format;

    public struct Formats
    {
        public byte Albedo;
        public byte Normal;
        public byte Metallic;
        public byte VertexAnimation;
        public byte VertexAnimationN;
    }
    
    public Options()
    {
        switch (ShowPrompt("Compression Types:\n0: Dark Souls 1 & 2\n1: Dark Souls 3\n2: Sekiro & Elden Ring (Requires oo2core_6_win64.dll)\nEnter the number of the compression to use: "))
        {
            case '0':
                Compression = DCX.Type.DCX_DFLT_10000_24_9;
                break;
            case '1':
                Compression = DCX.Type.DCX_DFLT_10000_44_9;
                break;
            case '2':
                Compression = DCX.Type.DCX_KRAK;
                break;
            default:
                throw new IOException("Not a valid compression type.");
        }

        if (ShowPrompt("Use dcx? (y/n): ") == 'y')
        {
            UseDcx = true;
        }

        switch (Compression)
        {
            case DCX.Type.DCX_DFLT_10000_24_9:
                Format.Normal = 0x6A;
                break;
            
            case DCX.Type.DCX_DFLT_10000_44_9:
                Format.Normal = 0x6A;
                break;

            case DCX.Type.DCX_KRAK:
                Format.Albedo = 0x66;
                Format.Normal = 0x6A;
                Format.Metallic = 0x67;
                Format.VertexAnimation = 0x16;
                Format.VertexAnimationN = 0x69;
                break;
        }
    }

    public static char ShowPrompt(string prompt)
    {
        Console.WriteLine(prompt);
        ConsoleKeyInfo keyChoice = Console.ReadKey();
        Console.WriteLine();
        return keyChoice.KeyChar;
    }
}