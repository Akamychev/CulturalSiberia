using System;
using System.Linq;
using System.Windows;
using CulturalSiberiaProject.Models;
using CulturalSiberiaProject.Services;

namespace CulturalSiberiaProject.Views.EventTemplates;

public partial class UpdateFilmEventWindow : Window
{
    private Cinema _cinema;
    
    public UpdateFilmEventWindow()
    {
        InitializeComponent();
    }
    
    private void CloseCurrentWindow_Click(object sender, RoutedEventArgs e)
    {
        Window.GetWindow(this).Close();
    }
    
    private void LoadCinemaDataButton_Click(object sender, RoutedEventArgs e)
    {
        int cinemaId;
        if (int.TryParse(CinemaId.Text, out cinemaId))
        {
            _cinema = Service.GetDbContext().Cinemas.FirstOrDefault(c => c.Id == cinemaId);
            if (_cinema != null)
            {
                RealiseDate.SelectedDate = _cinema.Realisedate.HasValue ? 
                    (DateTime?)_cinema.Realisedate.Value.ToDateTime(TimeOnly.MinValue) : null;
                Genre.Text = _cinema.Genre;
                Budget.Text = _cinema.Budget.ToString();
                Studio.Text = _cinema.Studio;
                Nameing.Text = _cinema.Nameing;
                Country.Text = _cinema.Contry;
                Languages.Text = _cinema.Languages;
                RunningTime.Text = _cinema.Runningtime;
            }
            else
                MessageBox.Show("Фильм с указанным ID не найден.");
        }
        else
            MessageBox.Show("Введите корректный ID фильма.");
    }
    
    private void UpdateCinemaButton_Click(object sender, RoutedEventArgs e)
    {
        if (_cinema != null)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(Genre.Text)
                    && !string.IsNullOrWhiteSpace(Nameing.Text)
                    && !string.IsNullOrWhiteSpace(Languages.Text))
                {

                    _cinema.Realisedate = RealiseDate.SelectedDate.HasValue
                        ? DateOnly.FromDateTime(RealiseDate.SelectedDate.Value)
                        : (DateOnly?)null;
                    _cinema.Genre = Genre.Text;
                    _cinema.Budget = int.Parse(Budget.Text);
                    _cinema.Studio = Studio.Text;
                    _cinema.Contry = Country.Text;
                    _cinema.Languages = Languages.Text;
                    _cinema.Runningtime = RunningTime.Text;

                    Service.GetDbContext().SaveChanges();

                    MessageBox.Show("Данные о фильме обновлены.");
                }
                else
                    MessageBox.Show("Обязательные поля не заполнены", "Ошибка обновления фильма",
                        MessageBoxButton.OK,MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при обновлении данных: {ex.Message}");
            }
        }
        else
            MessageBox.Show("Сначала загрузите данные о фильме.");
    }
}