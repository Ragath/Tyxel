﻿using System.Diagnostics;
using SkiaSharp;

namespace Tyxel;

static class PyxelProcessing
{
    public static void ProcessFile(ProjectConfig project)
    {
        var sw = Stopwatch.StartNew();
        while (true)
            try
            {
                var pyxelPath = project.GetPyxelPath();
                Console.WriteLine($"Processing {pyxelPath}");
                if (File.Exists(pyxelPath))
                    using (var stream = File.Open(pyxelPath, FileMode.Open, FileAccess.Read, FileShare.Read))
                    {
                        using (var doc = new PyxelParser.Document(stream))
                        {
                            var meta = doc.MetaData.Value;

                            bool sheetFinder(KeyValuePair<int, PyxelParser.Layer> l) => l.Value.Name.Trim().Equals("Sheet", StringComparison.InvariantCultureIgnoreCase);

                            if (!meta.Canvas.Layers.Any(sheetFinder))
                                throw new KeyNotFoundException("Could not find a layer named 'Sheet'");

                            var sheetLayer = meta.Canvas.Layers.Single(sheetFinder);

                            var columns = meta.Canvas.GetColumns();
                            var rows = meta.Canvas.GetRows();

                            using (var img = doc.GetImages(entry => SKBitmap.Decode(entry.Stream)).Single(i => i.Path == $"layer{sheetLayer.Key}.png").Value)
                            {
                                Directory.CreateDirectory(Path.GetDirectoryName(project.GetTilesetImagePath())!);
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
                            Directory.CreateDirectory(Path.GetDirectoryName(project.GetTilesetJsonPath())!);
                            File.WriteAllText(project.GetTilesetJsonPath(), JsonSerializer.Serialize(ts, new JsonSerializerOptions { WriteIndented = true }));
                        }
                    }

                break;
            }
            catch (IOException) //HACK: Wtf?
            {
                Thread.Sleep(10);
                if (sw.Elapsed > TimeSpan.FromSeconds(3))
                    throw;
            }
    }

    static void SaveSheet(TilesetConfig output, PyxelParser.PyxelData meta, int columns, int rows, SKBitmap img, string outPath)
    {
        if (output.Spacing == 0)
            img.Save(outPath);
        else
        {
            var w = columns * meta.Canvas.TileWidth + (columns - 1) * output.Spacing;
            var h = rows * meta.Canvas.TileHeight + (rows - 1) * output.Spacing;
            using var result = new SKBitmap(w, h, isOpaque: false);


            result.Draw(canvas =>
            {
                int DestX(int x) => x * meta.Canvas.TileWidth + x * output.Spacing;
                int DestY(int y) => y * meta.Canvas.TileHeight + y * output.Spacing;
                for (int y = 0; y < rows; y++)
                    for (int x = 0; x < columns; x++)
                    {
                        var src = meta.Canvas.GetCell(x, y).ToRect();
                        var dst = SKRect.Create(new(DestX(x), DestY(y)), src.Size);

                        canvas.DrawBitmap(img, src, dst);
                    }
            });

            result.Save(outPath);
        }
    }
}
