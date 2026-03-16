using SkiaSharp;
using SnowflakeApp.Helpers;

namespace SnowflakeApp;

public partial class PrintPage : ContentPage
{
    float _comp;
    float _angle;

    public PrintPage(float complexity, float angle)
    {
        InitializeComponent();
        _comp = complexity;
        _angle = angle;
        Title = "Подготовка к печати...";
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await Task.Delay(500);
        await GenerateAndSharePdf();
    }

    private async Task GenerateAndSharePdf()
    {
        string fileName = $"Snowflake_{DateTime.Now:yyyyMMdd_HHmmss}.pdf";
        string filePath = Path.Combine(FileSystem.CacheDirectory, fileName);

        using (var stream = File.Create(filePath))
        using (var document = SKDocument.CreatePdf(stream))
        {
            float width = 595;
            float height = 842;

            using (var canvas = document.BeginPage(width, height))
            {
                canvas.Clear(SKColors.White);

                using (var textPaint = new SKPaint { TextSize = 24, Color = SKColors.Black, IsAntialias = true, TextAlign = SKTextAlign.Center })
                {
                    canvas.DrawText("Новогодняя Снежинка", width / 2, 50, textPaint);
                }

                SnowflakeDrawer.Draw(canvas, width, height, _comp, _angle, SKColors.Black, 2f);

                using (var smallPaint = new SKPaint { TextSize = 12, Color = SKColors.Gray, TextAlign = SKTextAlign.Center })
                {
                    canvas.DrawText("Сгенерировано в приложении SnowflakeApp", width / 2, height - 30, smallPaint);
                }
            }
            document.EndPage();
        }

        await Share.Default.RequestAsync(new ShareFileRequest
        {
            Title = "Распечатай снежинку",
            File = new ShareFile(filePath)
        });

        await Navigation.PopAsync();
    }
}