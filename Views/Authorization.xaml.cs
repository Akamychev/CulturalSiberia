using System.Windows;
using CulturalSiberiaProject.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace CulturalSiberiaProject.Views;

public partial class Authorization : Window
{
    public Authorization()
    {
        InitializeComponent();
        this.DataContext = new AuthorizationModel();
    }
    
    private void OpenRegistrationWindow_click(object sender, RoutedEventArgs e)
    {
        var NewRegistrationWindow = new Registration();
        NewRegistrationWindow.Show();
        Window.GetWindow(this).Close();
    }
    
    
}