using System.Linq;
using System.Windows;
using CulturalSiberiaProject.Models;
using CulturalSiberiaProject.Services;

namespace CulturalSiberiaProject.Views.ShowPieceAllOperationWindows;

public partial class DeleteShowPieceWindow : Window
{
    public DeleteShowPieceWindow()
    {
        InitializeComponent();
    }
    
    private void CloseCurrentWindow_Click(object sender, RoutedEventArgs e)
    {
        Window.GetWindow(this).Close();
    }
    
    private void DeleteEventButton_Click(object sender, RoutedEventArgs e)
    {
        if (int.TryParse(ShowPieceId.Text, out int eventId))
        {
            var showPieceToDelete = Service.GetDbContext().Showpieces
                .FirstOrDefault(c => c.Id == eventId);
            if (showPieceToDelete != null)
            {
                var transactionConfirmation = MessageBox.Show(
                    "Вы уверены, что хотите удалить этот экспонат?", "Подтверждение удаления",
                    MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (transactionConfirmation == MessageBoxResult.Yes)
                {
                    Service.GetDbContext().Remove(showPieceToDelete);
                    Service.GetDbContext().SaveChanges();
                    MessageBox.Show("Экспонат удален.");
                }
                else
                    MessageBox.Show("Удаление отменено.");
            }
            else
                MessageBox.Show("Экспонат с указанным ID не найден.", "Ошибка удаления",
                    MessageBoxButton.OK, MessageBoxImage.Error);
        }
        else
            MessageBox.Show("Введите корректный ID экспоната.", "Ошибка удаления",
                MessageBoxButton.OK, MessageBoxImage.Error);
    }
   
}