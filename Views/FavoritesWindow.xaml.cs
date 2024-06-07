using System.Windows;
using CulturalSiberiaProject.ViewModels;

namespace CulturalSiberiaProject.Views;

public partial class FavoritesWindow : Window
{
    public FavoritesWindow()
    {
        InitializeComponent();
        this.DataContext = new FavoritesViewModel();
    }
    
    private void CloseCurrentWindow_Click(object sender, RoutedEventArgs e)
    {
        Window.GetWindow(this).Close();
    }
}