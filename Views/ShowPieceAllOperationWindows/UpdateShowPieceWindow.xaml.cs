using System;
using System.Linq;
using System.Windows;
using CulturalSiberiaProject.Models;
using CulturalSiberiaProject.Services;

namespace CulturalSiberiaProject.Views.ShowPieceAllOperationWindows;

public partial class UpdateShowPieceWindow : Window
{
    private Showpiece _showpiece;
    
    public UpdateShowPieceWindow()
    {
        InitializeComponent();
    }
    
    private void CloseCurrentWindow_Click(object sender, RoutedEventArgs e)
    {
        Window.GetWindow(this).Close();
    }
    
    private void LoadShowPieceDataButton_Click(object sender, RoutedEventArgs e)
    {
        int showPieceId;
        if (int.TryParse(ShowPieceId.Text, out showPieceId))
        {
            _showpiece = Service.GetDbContext().Showpieces.FirstOrDefault(c => c.Id == showPieceId);
            if (_showpiece != null)
            {
                Subject.Text = _showpiece.Subject;
                BornDate.SelectedDate = _showpiece.Borndate.HasValue ? (DateTime?)_showpiece.Borndate.Value.ToDateTime(TimeOnly.MinValue) : null;
                Originality.IsChecked = _showpiece.Originality;
                Nameing.Text = _showpiece.Nameing;
                Price.Text = _showpiece.Price.ToString();
                History.Text = _showpiece.History;
            }
            else
                MessageBox.Show("Экспонат с указанным ID не найден.");
        }
        else
            MessageBox.Show("Введите корректный ID экспоната.");
    }
    
    private void UpdateConcertButton_Click(object sender, RoutedEventArgs e)
    {
        if (_showpiece != null)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(Price.Text)
                    && !string.IsNullOrWhiteSpace(Nameing.Text))
                {
                    if (!decimal.TryParse(Price.Text, out decimal price))
                    {
                        MessageBox.Show("Не корректная цена", "Ошибка цены", 
                            MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        _showpiece.Price = int.Parse(Price.Text);
                        _showpiece.Borndate = BornDate.SelectedDate.HasValue
                            ? DateOnly.FromDateTime(BornDate.SelectedDate.Value)
                            : (DateOnly?)null;
                        _showpiece.Nameing = Nameing.Text;
                        _showpiece.History = History.Text;
                        _showpiece.Originality = Originality.IsChecked;
                        _showpiece.Subject = Subject.Text;

                        Service.GetDbContext().SaveChanges();
                        MessageBox.Show("Данные о экспонате обновлены.");
                    }
                }
                else
                    MessageBox.Show("Обязательные поля не заполнены", "Ошибка обновления экспоната",
                        MessageBoxButton.OK,MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при обновлении данных: {ex.Message}");
            }
        }
        else
            MessageBox.Show("Сначала загрузите данные о экспонате.");
    }
}