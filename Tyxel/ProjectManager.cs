namespace Tyxel;

static class ProjectManager
{
    public static ProjectConfig LoadProject(string path)
    {
        Console.WriteLine($"Opening project: {path}");
        var project = JsonSerializer.Deserialize<ProjectConfig>(File.ReadAllText(path), new JsonSerializerOptions(JsonSerializerDefaults.Web)
        {
            ReadCommentHandling = JsonCommentHandling.Skip
        }) ?? throw new NullReferenceException();

        project.Root = Path.GetDirectoryName(Path.GetFullPath(path));

        return project;
    }

    public static ProjectConfig CreateProject(string pyxelPath)
    {
        var projectPath = Path.ChangeExtension(Path.GetFullPath(pyxelPath), ".json");
        if (File.Exists(projectPath))
            return LoadProject(projectPath);
        else
        {
            var project = new ProjectConfig()
            {
                Pyxel = Path.GetFileName(pyxelPath),
                Root = Path.GetDirectoryName(Path.GetFullPath(projectPath)),
                Tileset = new TilesetConfig()
                {
                    Name = Path.GetFileNameWithoutExtension(projectPath) + "_Tileset",
                    OutputDir = "Output",
                    Spacing = 1
                }
            };
            Console.WriteLine($"Creating project: {projectPath}");
            File.WriteAllText(projectPath, JsonSerializer.Serialize(project, new JsonSerializerOptions { WriteIndented = true }));
            return project;
        }
    }

}
