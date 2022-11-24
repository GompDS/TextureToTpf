using SoulsFormats;

namespace TextureToTpf;

public static class Program
{
    public static void Main(string[] args)
    {
        Options op = new();
        
        foreach (string file in args.Where(x => (File.Exists(x) && x.EndsWith("dds")) || Directory.Exists(x)))
        {
            TPF newTpf = new();
            
            if (Directory.Exists(file))
            {
                string[] textures = Directory.EnumerateFiles(file, "*.dds").ToArray();
                if (textures.Any())
                { 
                    foreach (string texture in textures)
                    {
                        newTpf.AddTexture(texture, op);
                    }
                }

                if (!Directory.EnumerateFiles(file).Any())
                {
                    Directory.Delete(file);
                }
            }
            else
            {
                newTpf.AddTexture(file, op);
            }
            
            if (!op.UseDcx)
            {
                if (Directory.Exists(file))
                {
                    File.WriteAllBytes(file[..^1] + ".tpf", newTpf.Write());
                }
                else
                {
                    File.WriteAllBytes(Path.ChangeExtension(file, ".tpf"), newTpf.Write());
                }
            }
            else
            {
                byte[] dcxBytes = DCX.Compress(newTpf.Write(), op.Compression);
                if (Directory.Exists(file))
                {
                    File.WriteAllBytes(file[..^1] + ".tpf.dcx", dcxBytes);
                }
                else
                {
                    File.WriteAllBytes(Path.ChangeExtension(file, ".tpf.dcx"), dcxBytes);
                }
            }
        }
    }

    public static void AddTexture(this TPF tpf, string ddsPath, Options op)
    {
        byte format = 0;
        string fileName = Path.GetFileNameWithoutExtension(ddsPath);
        if (ddsPath.EndsWith("_a.dds", StringComparison.OrdinalIgnoreCase) || 
                 ddsPath.EndsWith("_a_l.dds", StringComparison.OrdinalIgnoreCase))
        {
            format = op.Format.Albedo;
        }
        else if (ddsPath.EndsWith("_n.dds", StringComparison.OrdinalIgnoreCase) || 
            ddsPath.EndsWith("_n_l.dds", StringComparison.OrdinalIgnoreCase))
        {
            format = op.Format.Normal;
        }
        else if (ddsPath.EndsWith("_m.dds", StringComparison.OrdinalIgnoreCase) || 
                 ddsPath.EndsWith("_m_l.dds", StringComparison.OrdinalIgnoreCase))
        {
            format = op.Format.Metallic;
        }
        else if (ddsPath.EndsWith("_vat.dds", StringComparison.OrdinalIgnoreCase) || 
                 ddsPath.EndsWith("_vat_l.dds", StringComparison.OrdinalIgnoreCase))
        {
            format = op.Format.VertexAnimation;
        }
        else if (ddsPath.EndsWith("_van.dds", StringComparison.OrdinalIgnoreCase) || 
                 ddsPath.EndsWith("_van_l.dds", StringComparison.OrdinalIgnoreCase))
        {
            format = op.Format.VertexAnimationN;
        }
        
        TPF.Texture newTexture = new(fileName, format, 0, File.ReadAllBytes(ddsPath));
        tpf.Textures.Add(newTexture);

        File.Delete(ddsPath);
    }
}