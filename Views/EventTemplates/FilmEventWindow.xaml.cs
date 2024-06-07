using System;
using System.Windows;
using CulturalSiberiaProject.Models;
using CulturalSiberiaProject.Services;

namespace CulturalSiberiaProject.Views;

public partial class FilmEventWindow : Window
{
    public FilmEventWindow()
    {
        InitializeComponent();
    }

    private void CloseCurrentWindow_Click(object sender, RoutedEventArgs e)
    {
        Window.GetWindow(this).Close();
    }

    private void AddFilmEvent_click(object sender, RoutedEventArgs e)
    {
        try
        {
            if (!string.IsNullOrWhiteSpace(Genre.Text)
                && !string.IsNullOrWhiteSpace(Nameing.Text)
                && !string.IsNullOrWhiteSpace(Languages.Text))
            {

                var realiseDate = RealiseDate.SelectedDate.HasValue
                    ? DateOnly.FromDateTime(RealiseDate.SelectedDate.Value)
                    : DateOnly.FromDateTime(DateTime.Now);

                var cinema = new Cinema
                {
                    Realisedate = realiseDate,
                    Genre = Genre.Text,
                    Budget = int.Parse(Budget.Text),
                    Nameing = Nameing.Text,
                    Studio = Studio.Text,
                    Contry = Country.Text,
                    Languages = Languages.Text,
                    Runningtime = RunningTime.Text,
                };

                Service.GetDbContext().Cinemas.Add(cinema);
                Service.GetDbContext().SaveChanges();
                
                var eventEntry = new Event()
                {
                    Cinemaid = cinema.Id
                };
                Service.GetDbContext().Events.Add(eventEntry);
                Service.GetDbContext().SaveChanges();

                MessageBox.Show("Фильм добавлен");
            }
            else
                MessageBox.Show("Обязательные поля не заполнены", "Ошибка добавления фильма", 
                    MessageBoxButton.OK,MessageBoxImage.Error);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка при добавлении фильма: {ex.Message}");
        }
    }
}