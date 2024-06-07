using System.Collections.Generic;
using System.Linq;
using System.Windows;
using CulturalSiberiaProject.Models;
using CulturalSiberiaProject.Services;
using CulturalSiberiaProject.ViewModels;
using CulturalSiberiaProject.Views.EmployeeAllOperationWindows;
using CulturalSiberiaProject.Views.EventTemplates;
using CulturalSiberiaProject.Views.ShowPieceAllOperationWindows;
using CulturalSiberiaProject.Views.PdfReportsForToolkit;

namespace CulturalSiberiaProject.Views;

public partial class ToolKit : Window
{
    private Dictionary<string, Window> _eventWindows;
    public ToolKit()
    {
        InitializeComponent();
        LoadEventTypes();
        InitializeEventWindows();
    }

    private void CloseCurrentWindow_click(object sender, RoutedEventArgs e)
    {
        Window.GetWindow(this).Close();
    }

    private void LoadEventTypes()
    {
        var eventTypes = new List<string>
        {
            "Кино",
            "Концерт"
        };

        EventTypeListBox.ItemsSource = eventTypes;
    }

    private void InitializeEventWindows()
    {
        _eventWindows = new Dictionary<string, Window>()
        {
            { "Кино", new FilmEventWindow() },
            { "Концерт", new ConcertEventWindow() }
        };
    }

    private void EventTypeListBox_SelectionChanged(object sender, RoutedEventArgs e)
    {
        
    }

    private void AddEventButton_Click(object sender, RoutedEventArgs e)
    {
        var selectedEventType = (string)EventTypeListBox.SelectedItem;
        if (!string.IsNullOrWhiteSpace(selectedEventType) && _eventWindows.ContainsKey(selectedEventType))
        {
            var CurrentTemplate = _eventWindows[selectedEventType];
            CurrentTemplate.ShowDialog();
        }
        else
            MessageBox.Show("Выберите тип мероприятия", "Ошибка добавления", 
                MessageBoxButton.OK,MessageBoxImage.Error);
    }

    private void UpdateEventButton_Click(object sender, RoutedEventArgs e)
    {
        var selectedEventType = (string)EventTypeListBox.SelectedItem;
        if (!string.IsNullOrWhiteSpace(selectedEventType))
        {
            Window updateWindow = null;
            switch (selectedEventType)
            {
                case "Кино":
                    updateWindow = new UpdateFilmEventWindow();
                    break;
                
                case "Концерт":
                    updateWindow = new UpdateConcertEventWindow();
                    break;
            }
                updateWindow.ShowDialog();
        }
        else
            MessageBox.Show("Выберите тип мероприятия", "Ошибка обновления", 
                MessageBoxButton.OK, MessageBoxImage.Error);
    }

    private void DeleteEventButton_Click(object sender, RoutedEventArgs e)
    {
        var selectedEventType = (string)EventTypeListBox.SelectedItem;
        if (!string.IsNullOrWhiteSpace(selectedEventType))
        {
            var deleteEventWindow = new DeleteEventWindow(selectedEventType);
            deleteEventWindow.ShowDialog();
        }
        else
        {
            MessageBox.Show("Выберите тип мероприятия", "Ошибка удаления",
                MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private void OpenAddShowPieceWindow_Click(object sender, RoutedEventArgs e)
    {
        var newAddShowPieceWindow = new AddShowPieceWindow();
        newAddShowPieceWindow.ShowDialog();
    }
    
    private void OpenUpdateShowPieceWindow_Click(object sender, RoutedEventArgs e)
    {
        var newUpdateShowPieceWindow = new UpdateShowPieceWindow();
        newUpdateShowPieceWindow.ShowDialog();
    }
    
    private void OpenDeleteShowPieceWindow_Click(object sender, RoutedEventArgs e)
    {
        var newDeleteShowPieceWindow = new DeleteShowPieceWindow();
        newDeleteShowPieceWindow.ShowDialog();
    }

    private void PdfReportShowPiece_click(object sender, RoutedEventArgs e)
    {
        List<Showpiece> data = Service.GetDbContext().Showpieces.ToList();
        ReportShowPiece.PrintPdfReport(data);
    }

    private void OpenAddEmployeeWindow_Click(object sender, RoutedEventArgs e)
    {
        var newAddEmployeeWindow = new CreateEmployeeWindow();
        newAddEmployeeWindow.Show();
    }
    
    private void OpenUpdateEmployeeWindow_Click(object sender, RoutedEventArgs e)
    {
        var newUpdateEmployeeWindow = new UpdateEmployeeWindow();
        newUpdateEmployeeWindow.Show();
    }
    
    private void OpenDeleteEmployeeWindow_Click(object sender, RoutedEventArgs e)
    {
        var newDeleteEmployeeWindow = new DeleteEmployeeWindow();
        newDeleteEmployeeWindow.Show();
    }
    
    private void PdfReportEmployeeAndAdmin_click(object sender, RoutedEventArgs e)
    {
        List<User> data = Service.GetDbContext().Users.ToList();
        ReportEmploeesAndAdmins.PrintPdfReport(data);
    }
}