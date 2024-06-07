using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CulturalSiberiaProject.Models;
using CulturalSiberiaProject.ViewModels;

namespace CulturalSiberiaProject.Views;

public partial class MainView : Window
{
    public MainView(MainViewModel viewModel)
    {
        InitializeComponent();
        this.DataContext = viewModel;
        viewModel.LoadEvents();
    }

    private void OpenToolKit_click(object sender, RoutedEventArgs e)
    {
        var NewToolKitWindow = new ToolKit();
        NewToolKitWindow.Show();
    }

    private void OpenPersonalCabienet_Click(object sender, RoutedEventArgs e)
    {
        var newPersonalCabienetWindow = new PersonalCabinet();
        newPersonalCabienetWindow.Show();
    }
    
    private void OpenFavorites_Click(object sender, RoutedEventArgs e)
    {
        var newFavoritesWindow = new FavoritesWindow();
        newFavoritesWindow.Show();
    }
    
    private void OnEventDoubleClick(object sender, MouseButtonEventArgs e)
    {
        if (sender is ListView listView && listView.SelectedItem is Event selectedEvent)
        {
            var viewModel = DataContext as MainViewModel;
            viewModel?.OpenEventDetailsCommand.Execute(selectedEvent);
        }
    }
}