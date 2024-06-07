using System;
using System.Windows;
using CulturalSiberiaProject.Models;
using CulturalSiberiaProject.Services;

namespace CulturalSiberiaProject.Views;

public partial class ConcertEventWindow : Window
{
    public ConcertEventWindow()
    {
        InitializeComponent();
    }
    
    private void CloseCurrentWindow_Click(object sender, RoutedEventArgs e)
    {
        Window.GetWindow(this).Close();
    }
    
    private void AddConcertEvent_click(object sender, RoutedEventArgs e)
    {
        try
        {
            if (!string.IsNullOrWhiteSpace(NumberOfSeats.Text)
                && !string.IsNullOrWhiteSpace(Nameing.Text)
                && !string.IsNullOrWhiteSpace(DurationInMinutes.Text))
            {

                var concertDate = ConcertDate.SelectedDate.HasValue
                    ? DateOnly.FromDateTime(ConcertDate.SelectedDate.Value)
                    : DateOnly.FromDateTime(DateTime.Now);

                var concert = new Concert()
                {
                    Concertdate = concertDate,
                    Duration = int.Parse(DurationInMinutes.Text),
                    Numberofseats = int.Parse(NumberOfSeats.Text),
                    Programofconcert = Nameing.Text
                };
                
                Service.GetDbContext().Concerts.Add(concert);
                Service.GetDbContext().SaveChanges();
                
                var eventEntry = new Event()
                {
                    Concertid = concert.Id
                };
                
                Service.GetDbContext().Events.Add(eventEntry);
                Service.GetDbContext().SaveChanges();

                MessageBox.Show("Концерт добавлен");
            }
            else
                MessageBox.Show("Обязательные поля не заполнены", "Ошибка добавления концерта",
                    MessageBoxButton.OK,MessageBoxImage.Error);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка при добавлении концерта: {ex.Message}");
        }
    }
}