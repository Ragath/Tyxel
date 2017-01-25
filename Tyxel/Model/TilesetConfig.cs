namespace Tyxel.Model
{
    public class TilesetConfig
    {
        public string OutputDir { get; set; }
        public string Name { get; set; }
        public ushort Spacing { get; set; }

        public string ImageFile => $"{Name}.png";
        public string TilesetFile => $"{Name}.json";
    }
}
