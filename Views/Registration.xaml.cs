using System.Windows;
using CulturalSiberiaProject.ViewModels;

namespace CulturalSiberiaProject.Views;

public partial class Registration : Window
{
    public Registration()
    {
        InitializeComponent();
        this.DataContext = new RegistrationModel();
    }
    
    private void CloseRegistrationWindow(object sender, RoutedEventArgs e)
    {
        var NewAuthorizationWindow = new Authorization();
        NewAuthorizationWindow.Show();
        Window.GetWindow(this).Close();
    }
}