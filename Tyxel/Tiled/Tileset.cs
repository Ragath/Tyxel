namespace Tyxel.Tiled;


public class Tileset
{
    public int firstgid { get; set; }

    [JsonPropertyName("columns")]
    public int Columns { get; set; }

    public int imageheight { get; set; }
    [JsonPropertyName("image")]
    public string? ImagePath { get; set; }
    public int imagewidth { get; set; }
    public int margin { get; set; }
    public string name { get; set; } = null!;

    [JsonPropertyName("properties")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Dictionary<string, string>? Properties { get; set; }

    public int spacing { get; set; }

    [JsonPropertyName("tilecount")]
    public int TileCount { get; set; }

    public int tileheight { get; set; }

    [JsonPropertyName("tileproperties")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Dictionary<int, Dictionary<string, string>>? TileProperties { get; set; }

    public int tilewidth { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? transparentcolor { get; set; }
}
