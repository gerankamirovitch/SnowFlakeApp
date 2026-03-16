using SkiaSharp;
using SnowflakeApp.Helpers;
using SnowflakeApp.Models;

namespace SnowflakeApp;

public partial class MainPage : ContentPage
{
    struct Particle { public float X; public float Y; public float Speed; public float Size; }
    List<Particle> particles = new List<Particle>();
    IDispatcherTimer timer;
    Random rnd = new Random();

    public MainPage()
    {
        InitializeComponent();

        for (int i = 0; i < 100; i++)
            particles.Add(new Particle { X = rnd.Next(0, 1000), Y = rnd.Next(0, 2000), Speed = rnd.Next(2, 6), Size = rnd.Next(2, 5) });

        timer = Dispatcher.CreateTimer();
        timer.Interval = TimeSpan.FromMilliseconds(16);
        timer.Tick += (s, e) => {
            for (int i = 0; i < particles.Count; i++)
            {
                var p = particles[i];
                p.Y += p.Speed;
                if (p.Y > Application.Current.MainPage.Height) p.Y = -10;
                particles[i] = p;
            }
            SnowView.InvalidateSurface();
        };
        timer.Start();
    }

    private void OnSnowflakePaint(object sender, SkiaSharp.Views.Maui.SKPaintSurfaceEventArgs e)
    {
        var canvas = e.Surface.Canvas;
        canvas.Clear();

        SnowflakeDrawer.Draw(canvas, e.Info.Width, e.Info.Height,
            (float)SliderComplexity.Value,
            (float)SliderAngle.Value,
            SKColors.White,
            3f);
    }

    private void OnSnowPaint(object sender, SkiaSharp.Views.Maui.SKPaintSurfaceEventArgs e)
    {
        var canvas = e.Surface.Canvas;
        canvas.Clear();

        using (var paint = new SKPaint { Style = SKPaintStyle.Fill, Color = SKColors.White.WithAlpha(200) })
        {
            foreach (var p in particles)
            {
                canvas.DrawCircle(p.X % e.Info.Width, p.Y, p.Size, paint);
            }
        }

    }

    private void OnSliderChanged(object sender, ValueChangedEventArgs e) => SnowflakeView.InvalidateSurface();

    private void OnSaveClicked(object sender, EventArgs e)
    {
        App.Gallery.Add(new SnowflakeItem
        {
            Name = $"Снежинка {DateTime.Now:HH:mm}",
            Complexity = (float)SliderComplexity.Value,
            BranchAngle = (float)SliderAngle.Value,
            DateCreated = DateTime.Now
        });
        DisplayAlert("Ура!", "Снежинка сохранена в мешок Деда Мороза!", "ОК");
    }

    private async void OnPrintClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new PrintPage((float)SliderComplexity.Value, (float)SliderAngle.Value));
    }

    private async void OnGoToGalleryClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new GalleryPage());
    }
}