using SnowflakeApp.Models;

namespace SnowflakeApp;

public partial class App : Application
{
    public static List<SnowflakeItem> Gallery = new List<SnowflakeItem>
    {
        new SnowflakeItem { Name = "Первая искра", Complexity = 3, BranchAngle = 45, IsFavorite = true, DateCreated = DateTime.Now },
        new SnowflakeItem { Name = "Ледяной лес", Complexity = 5, BranchAngle = 25, IsFavorite = false, DateCreated = DateTime.Now }
    };

    public App()
    {
        InitializeComponent();
        MainPage = new NavigationPage(new MainPage());
    }
}