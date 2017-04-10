using System;
using System.IO;
using Newtonsoft.Json;
using Tyxel.Model;

namespace Tyxel
{
    static class ProjectManager
    {
        public static ProjectConfig LoadProject(string path)
        {
            Console.WriteLine($"Opening project: {path}");
            var project = JsonConvert.DeserializeObject<ProjectConfig>(File.ReadAllText(path));
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
                File.WriteAllText(projectPath, JsonConvert.SerializeObject(project, Formatting.Indented));
                return project;
            }
        }

    }
}
