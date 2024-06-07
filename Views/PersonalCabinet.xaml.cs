using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using CulturalSiberiaProject.Models;
using CulturalSiberiaProject.Services;
using CulturalSiberiaProject.ViewModels;

namespace CulturalSiberiaProject.Views;

public partial class PersonalCabinet : Window
{
    public List<Ticket> Tickets { get; set; } = new List<Ticket>();
    public PersonalCabinet()
    {
        InitializeComponent();
        DataContext = new PersonalCabienetViewModel();
    }
    
    private void CloseCurrentWindow_click(object sender, RoutedEventArgs e)
    {
        Window.GetWindow(this).Close();
    }

    private void OpenEditDataProfile_Click(object sender, RoutedEventArgs e)
    {
        var newOpenEditDataProfile = new EditUserDataFromPersonalCabienet(Service.GetCurrentUser());
        newOpenEditDataProfile.Show();
        Window.GetWindow(this).Close();
    }
    
    private void OpenTicketWindow_click(object sender, RoutedEventArgs e)
    {
        var newTicketWindow = new TicketView();
        newTicketWindow.Show();
    }
}