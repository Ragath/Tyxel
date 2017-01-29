using Newtonsoft.Json;

namespace Tyxel.Model
{
    [JsonConverter(typeof(ProjectConfigConverter))]
    public class ProjectConfig
    {
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public int Version { get; set; } = ProjectConfigConverter.Migrations.Count;
        public string Pyxel { get; set; }
        public TilesetConfig Tileset { get; set; }

        [JsonIgnore]
        public string Root { get; set; }
    }
}
