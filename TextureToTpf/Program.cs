using SoulsFormats;

namespace TextureToTpf;

public static class Program
{
    public static void Main(string[] args)
    {
        Options op = new();
        
        foreach (string file in args.Where(x => File.Exists(x) && x.EndsWith("dds")))
        {
            byte format = 0;
            string fileName = Path.GetFileNameWithoutExtension(file);
            if (file.EndsWith("_n.dds", StringComparison.OrdinalIgnoreCase))
            {
                format = 0x6A;
            }

            TPF.Texture newTexture = new(fileName, format, 0, File.ReadAllBytes(file));
            TPF newTpf = new();
            newTpf.Textures.Add(newTexture);

            if (op.Compression == DCX.Type.None)
            {
                File.WriteAllBytes(Path.ChangeExtension(file, ".tpf"), newTpf.Write());
            }
            else
            {
                byte[] dcxBytes = DCX.Compress(newTpf.Write(), op.Compression);
                File.WriteAllBytes(Path.ChangeExtension(file, ".tpf.dcx"), dcxBytes);
            }
            
            File.Delete(file);
        }
    }
}