using System.Drawing;
using PyxelParser;
using SkiaSharp;

namespace Tyxel;

static class PyxelExtensions
{
    public static int GetColumns(this Canvas canvas) => canvas.Width / canvas.TileWidth;
    public static int GetRows(this Canvas canvas) => canvas.Height / canvas.TileHeight;

    public static Rectangle GetCell(this Canvas canvas, int x, int y) => new()
    {
        X = x * canvas.TileWidth,
        Y = y * canvas.TileHeight,
        Width = canvas.TileWidth,
        Height = canvas.TileHeight
    };
    public static SKRect ToRect(this Rectangle rect) => new()
    {
        Left = rect.Left,
        Top = rect.Top,
        Bottom = rect.Bottom,
        Right = rect.Right
    };
}
