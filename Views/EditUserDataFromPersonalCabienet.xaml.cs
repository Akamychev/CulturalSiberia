using System.Windows;
using CulturalSiberiaProject.Models;
using CulturalSiberiaProject.ViewModels;

namespace CulturalSiberiaProject.Views;

public partial class EditUserDataFromPersonalCabienet : Window
{
    public EditUserDataFromPersonalCabienet(User user)
    {
        InitializeComponent();
        DataContext = new EditUserDataFromPersonalCabienetViewModel(user);
    }
    
    private void CloseCurrentWindowAndOpenProfile_Click(object sender, RoutedEventArgs e)
    {
        var newProfileWindow = new PersonalCabinet();
        newProfileWindow.Show();
        Window.GetWindow(this).Close();
    }
}