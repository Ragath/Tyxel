using System.IO;

namespace Tyxel
{
    static class ProjectExtensions
    {
        public static string GetOutputDir(this Model.ProjectConfig project) 
            => Path.IsPathRooted(project.Tileset.OutputDir) ? project.Tileset.OutputDir : Path.Combine(project.Root, project.Tileset.OutputDir);
        public static string GetPyxelPath(this Model.ProjectConfig project) 
            => Path.IsPathRooted(project.Pyxel) ? project.Pyxel : Path.Combine(project.Root, project.Pyxel);
        public static string GetTilesetJsonPath(this Model.ProjectConfig project)
            => Path.Combine(project.GetOutputDir(), project.Tileset.TilesetFile);
        public static string GetTilesetImagePath(this Model.ProjectConfig project)
            => Path.Combine(project.GetOutputDir(), project.Tileset.ImageFile);
    }
}
