namespace Tyxel.Model;

public class ProjectConfig : IJsonOnDeserialized, IJsonOnDeserializing
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public int Version { get; set; } = Migrations.CurrentVersion;
    public string Pyxel { get; set; } = null!;
    public TilesetConfig Tileset { get; set; } = null!;

    [JsonIgnore]
    public string? Root { get; set; }

    public void OnDeserialized() => Migrations.ApplyMigrations(this);
    public void OnDeserializing() => Version = default;
}
