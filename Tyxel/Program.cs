﻿using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Tyxel.Model;

namespace Tyxel
{
    class Program
    {
        static void Main(string[] args)
        {
            if(!args.Any())
            {
                Console.WriteLine("Please use this program to open a <project>.json file.");
                Console.WriteLine("Drag & drop the *.json file onto the .exe is one way of doing so.");
                return;
            }

            var projectPath = args.Single();
            var project = JsonConvert.DeserializeObject<ProjectConfig>(File.ReadAllText(projectPath));
            project.Root = Path.GetDirectoryName(Path.GetFullPath(projectPath));

            var pyxelPath = project.Pyxel;
            Console.WriteLine($"Observing: {pyxelPath}");
            using (var watcher = new Watcher(pyxelPath, filename => ProcessFile(project)))
            {
                while (true)
                    watcher.WaitForChange();
            }
        }


        static void ProcessFile(ProjectConfig project)
        {
            var sw = Stopwatch.StartNew();
            while (true)
                try
                {
                    var pyxelPath = project.GetPyxelPath();
                    Console.WriteLine($"Processing { pyxelPath}");
                    if (File.Exists(pyxelPath))
                        using (var stream = File.Open(pyxelPath, FileMode.Open, FileAccess.Read, FileShare.Read))
                        {
                            using (var doc = new PyxelParser.Document(stream))
                            {
                                var meta = doc.MetaData.Value;
                                var sheetLayer = meta.Canvas.Layers.Single(l => l.Value.Name.Trim().Equals("Sheet", StringComparison.InvariantCultureIgnoreCase));

                                var columns = meta.Canvas.GetColumns();
                                var rows = meta.Canvas.GetRows();

                                using (var img = doc.GetImages(entry => Image.FromStream(entry.Stream)).Single(i => i.Path == $"layer{sheetLayer.Key}.png").Value)
                                {
                                    Directory.CreateDirectory(Path.GetDirectoryName(project.GetTilesetImagePath()));
                                    var imgPath = project.GetTilesetImagePath();
                                    SaveSheet(project.Tileset, meta, columns, rows, img, imgPath);
                                }

                                var ts = new Tiled.Tileset()
                                {
                                    firstgid = 1,
                                    Columns = columns,
                                    ImagePath = project.Tileset.ImageFile,
                                    imagewidth = meta.Canvas.Width,
                                    imageheight = meta.Canvas.Height,
                                    margin = 0,
                                    name = project.Tileset.Name,
                                    spacing = project.Tileset.Spacing,
                                    TileCount = columns * rows,
                                    tileheight = meta.Canvas.TileHeight,
                                    tilewidth = meta.Canvas.TileWidth,
                                    transparentcolor = null
                                };
                                Directory.CreateDirectory(Path.GetDirectoryName(project.GetTilesetJsonPath()));
                                File.WriteAllText(project.GetTilesetJsonPath(), JsonConvert.SerializeObject(ts, Formatting.Indented));
                            }
                        }

                    break;
                }
                catch (IOException)
                {
                    System.Threading.Thread.Sleep(10);
                    if (sw.Elapsed > TimeSpan.FromSeconds(3))
                        throw;
                }
        }

        static void SaveSheet(TilesetConfig output, PyxelParser.PyxelData meta, int columns, int rows, Image img, string outPath)
        {
            if (output.Spacing == 0)
                img.Save(outPath);
            else
            {
                using (var result = new Bitmap(columns * meta.Canvas.TileWidth + (columns - 1) * output.Spacing, rows * meta.Canvas.TileHeight + (rows - 1) * output.Spacing))
                {
                    using (var gfx = Graphics.FromImage(result))
                    {
                        int DestX(int x) => x * meta.Canvas.TileWidth + x * output.Spacing;
                        int DestY(int y) => y * meta.Canvas.TileHeight + y * output.Spacing;
                        for (int y = 0; y < rows; y++)
                            for (int x = 0; x < columns; x++)
                                gfx.DrawImage(img, DestX(x), DestY(y), meta.Canvas.GetCell(x, y), GraphicsUnit.Pixel);
                        gfx.Flush();
                    }
                    result.Save(outPath);
                }
            }
        }
    }
}
