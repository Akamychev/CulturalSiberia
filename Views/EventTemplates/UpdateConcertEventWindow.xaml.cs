using System;
using System.Linq;
using System.Windows;
using CulturalSiberiaProject.Models;
using CulturalSiberiaProject.Services;

namespace CulturalSiberiaProject.Views.EventTemplates;

public partial class UpdateConcertEventWindow : Window
{
    private Concert _concert;
    
    public UpdateConcertEventWindow()
    {
        InitializeComponent();
    }
    
    private void CloseCurrentWindow_Click(object sender, RoutedEventArgs e)
    {
        Window.GetWindow(this).Close();
    }
    
    private void LoadConcertDataButton_Click(object sender, RoutedEventArgs e)
    {
        int concertId;
        if (int.TryParse(ConcertId.Text, out concertId))
        {
            _concert = Service.GetDbContext().Concerts.FirstOrDefault(c => c.Id == concertId);
            if (_concert != null)
            {
                NumberOfSeats.Text = _concert.Numberofseats.ToString();
                ConcertDate.SelectedDate = _concert.Concertdate.HasValue ? (DateTime?)_concert.Concertdate.Value.ToDateTime(TimeOnly.MinValue) : null;
                DurationInMinutes.Text = _concert.Duration.ToString();
                Nameing.Text = _concert.Programofconcert;
            }
            else
                MessageBox.Show("Концерт с указанным ID не найден.");
        }
        else
            MessageBox.Show("Введите корректный ID концерта.");
    }
    
    private void UpdateConcertButton_Click(object sender, RoutedEventArgs e)
    {
        if (_concert != null)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(NumberOfSeats.Text)
                    && !string.IsNullOrWhiteSpace(Nameing.Text)
                    && !string.IsNullOrWhiteSpace(DurationInMinutes.Text))
                {

                    _concert.Numberofseats = int.Parse(NumberOfSeats.Text);
                    _concert.Concertdate = ConcertDate.SelectedDate.HasValue
                        ? DateOnly.FromDateTime(ConcertDate.SelectedDate.Value)
                        : (DateOnly?)null;
                    _concert.Duration = int.Parse(DurationInMinutes.Text);
                    _concert.Programofconcert = Nameing.Text;

                    Service.GetDbContext().SaveChanges();

                    MessageBox.Show("Данные о концерте обновлены.");
                }
                else
                    MessageBox.Show("Обязательные поля не заполнены", "Ошибка обновления концерта",
                        MessageBoxButton.OK,MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при обновлении данных: {ex.Message}");
            }
        }
        else
            MessageBox.Show("Сначала загрузите данные о концерте.");
    }
}