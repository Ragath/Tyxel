using System.Collections.Generic;
using Newtonsoft.Json;

namespace Tyxel.Tiled
{

    public class Tileset
    {
        public int firstgid { get; set; }

        [JsonProperty("columns")]
        public int Columns { get; set; }

        public int imageheight { get; set; }
        [JsonProperty("image")]
        public string ImagePath { get; set; }
        public int imagewidth { get; set; }
        public int margin { get; set; }
        public string name { get; set; }

        [JsonProperty("properties", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, string> Properties { get; set; }

        public int spacing { get; set; }

        [JsonProperty("tilecount")]
        public int TileCount { get; set; }

        public int tileheight { get; set; }

        [JsonProperty("tileproperties", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<int, Dictionary<string, string>> TileProperties { get; set; }

        public int tilewidth { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string transparentcolor { get; set; }
    }
}
