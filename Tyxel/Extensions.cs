using SkiaSharp;

namespace Tyxel;
static class Extensions
{

    public static void Draw(this SKBitmap bitmap, Action<SKCanvas> action)
    {
        if (!bitmap.ReadyToDraw)
            throw new Exception(nameof(bitmap.ReadyToDraw));

        using var canvas = new SKCanvas(bitmap);
        action(canvas);
        canvas.Flush();
    }


    public static void Save(this SKBitmap bitmap, string path)
    {
        using var stream = File.Create(path);
        if (!bitmap.Encode(stream, SKEncodedImageFormat.Png, 100))
            throw new Exception(nameof(bitmap.Encode));
    }
}
