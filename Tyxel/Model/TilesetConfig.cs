namespace Tyxel.Model;

public class TilesetConfig
{
    public string OutputDir { get; set; } = null!;
    public string Name { get; set; } = null!;
    public ushort Spacing { get; set; }

    public string ImageFile => $"{Name}.png";
    public string TilesetFile => $"{Name}.json";
}
