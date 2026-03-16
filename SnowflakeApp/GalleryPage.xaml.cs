using SnowflakeApp.Models;
using SnowflakeApp.Helpers;

namespace SnowflakeApp;

public partial class GalleryPage : ContentPage
{
    public GalleryPage()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        CollView.ItemsSource = null;
        CollView.ItemsSource = App.Gallery;
    }

    private async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is SnowflakeItem item)
        {
            bool answer = await DisplayAlert("Выбор", "Распечатать эту снежинку?", "Да, в PDF", "Нет");
            if (answer)
            {
                await Navigation.PushAsync(new PrintPage(item.Complexity, item.BranchAngle));
            }
            CollView.SelectedItem = null;
        }
    }
}