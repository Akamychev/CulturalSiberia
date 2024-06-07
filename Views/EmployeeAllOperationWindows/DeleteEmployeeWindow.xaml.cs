using System.Linq;
using System.Windows;
using CulturalSiberiaProject.Services;

namespace CulturalSiberiaProject.Views.EmployeeAllOperationWindows;

public partial class DeleteEmployeeWindow : Window
{
    public DeleteEmployeeWindow()
    {
        InitializeComponent();
    }
    
    private void CloseCurrentWindow_Click(object sender, RoutedEventArgs e)
    {
        Window.GetWindow(this).Close();
    }
    
    private void DeleteEventButton_Click(object sender, RoutedEventArgs e)
    {
        if (int.TryParse(EmployeeId.Text, out int eventId))
        {
            var employeeToDelete = Service.GetDbContext().Users
                .FirstOrDefault(c => c.Id == eventId);
            if (employeeToDelete != null)
            {
                var transactionConfirmation = MessageBox.Show(
                    "Вы уверены, что хотите удалить этот экспонат?", "Подтверждение удаления",
                    MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (transactionConfirmation == MessageBoxResult.Yes)
                {
                    Service.GetDbContext().Remove(employeeToDelete);
                    Service.GetDbContext().SaveChanges();
                    MessageBox.Show("Сотрудник удален.");
                }
                else
                    MessageBox.Show("Удаление отменено.");
            }
            else
                MessageBox.Show("Сотрудник с указанным ID не найден.", "Ошибка удаления",
                    MessageBoxButton.OK, MessageBoxImage.Error);
        }
        else
            MessageBox.Show("Введите корректный ID сотрудника.", "Ошибка удаления",
                MessageBoxButton.OK, MessageBoxImage.Error);
    }
}