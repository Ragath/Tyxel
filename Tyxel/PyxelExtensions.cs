using System.Drawing;
using PyxelParser;

namespace Tyxel
{
    static class PyxelExtensions
    {
        public static int GetColumns(this Canvas canvas) => canvas.Width / canvas.TileWidth;
        public static int GetRows(this Canvas canvas) => canvas.Height / canvas.TileHeight;

        public static Rectangle GetCell(this Canvas canvas, int x, int y) => new Rectangle
        {
            X = x * canvas.TileWidth,
            Y = y * canvas.TileHeight,
            Width = canvas.TileWidth,
            Height = canvas.TileHeight
        };
    }
}
