using SkiaSharp;

namespace SnowflakeApp.Helpers
{
    public static class SnowflakeDrawer
    {
        public static void Draw(SKCanvas canvas, float width, float height, float complexity, float branchAngle, SKColor color, float strokeWidth)
        {
            float centerX = width / 2;
            float centerY = height / 2;
            float radius = Math.Min(width, height) / 2 * 0.8f;

            using (var paint = new SKPaint())
            {
                paint.Style = SKPaintStyle.Stroke;
                paint.Color = color;
                paint.StrokeWidth = strokeWidth;
                paint.IsAntialias = true;
                paint.StrokeCap = SKStrokeCap.Round;

                canvas.Translate(centerX, centerY);

                for (int i = 0; i < 6; i++)
                {
                    canvas.Save();
                    canvas.RotateDegrees(i * 60);
                    DrawBranch(canvas, 0, 0, radius, complexity, branchAngle, paint);

                    canvas.Scale(-1, 1);
                    DrawBranch(canvas, 0, 0, radius, complexity, branchAngle, paint);
                    canvas.Restore();
                }
            }
        }

        private static void DrawBranch(SKCanvas canvas, float x, float y, float length, float complexity, float angle, SKPaint paint)
        {
            if (length < 10) return;

            canvas.DrawLine(x, y, x, y - length, paint);

            canvas.Translate(0, -length / 3);

            if (complexity > 1)
            {
                canvas.Save();
                canvas.RotateDegrees(angle);
                canvas.Scale(0.7f);
                DrawBranch(canvas, 0, 0, length, complexity - 1, angle, paint);
                canvas.Restore();

                canvas.Save();
                canvas.RotateDegrees(-angle);
                canvas.Scale(0.7f);
                DrawBranch(canvas, 0, 0, length, complexity - 1, angle, paint);
                canvas.Restore();
            }
        }
    }
}