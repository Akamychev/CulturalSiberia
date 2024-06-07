using System;
using System.Windows;
using CulturalSiberiaProject.Models;
using CulturalSiberiaProject.Services;

namespace CulturalSiberiaProject.Views.ShowPieceAllOperationWindows;

public partial class AddShowPieceWindow : Window
{
    public AddShowPieceWindow()
    {
        InitializeComponent();
    }
    
    private void CloseCurrentWindow_Click(object sender, RoutedEventArgs e)
    {
        Window.GetWindow(this).Close();
    }
    
    private void AddShowPieceEvent_click(object sender, RoutedEventArgs e)
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


                    var foundationDate = BornDate.SelectedDate.HasValue
                        ? DateOnly.FromDateTime(BornDate.SelectedDate.Value)
                        : DateOnly.FromDateTime(DateTime.Now);

                    var ShowPiece = new Showpiece()
                    {
                        Borndate = foundationDate,
                        Nameing = Nameing.Text,
                        Price = int.Parse(Price.Text),
                        History = History.Text,
                        Subject = Subject.Text,
                        Originality = Originality.IsChecked == true
                    };

                    Service.GetDbContext().Showpieces.Add(ShowPiece);
                    Service.GetDbContext().SaveChanges();

                    MessageBox.Show("Экспонат добавлен");
                }
                
            }
            else
                MessageBox.Show("Обязательные поля не заполнены", "Ошибка добавления экспоната", 
                    MessageBoxButton.OK,MessageBoxImage.Error);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка при добавлении экспоната: {ex.Message}");
        }
    }
}