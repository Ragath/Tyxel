using Newtonsoft.Json;

namespace Tyxel.Model
{
    public class ProjectConfig
    {
        public string Pyxel { get; set; }
        public TilesetConfig Tileset { get; set; }

        [JsonIgnore]
        public string Root { get; set; }
    }
}
