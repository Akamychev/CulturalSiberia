using System.Linq;
using System.Windows;
using CulturalSiberiaProject.Services;
using Microsoft.EntityFrameworkCore;

namespace CulturalSiberiaProject.Views.EventTemplates;

public partial class DeleteEventWindow : Window
{
    private readonly string _eventType;
    public DeleteEventWindow(string eventType)
    {
        InitializeComponent();
        _eventType = eventType;
    }
    
    private void CloseCurrentWindow_Click(object sender, RoutedEventArgs e)
    {
        Window.GetWindow(this).Close();
    }
    
    private void DeleteEventButton_Click(object sender, RoutedEventArgs e)
    {
        if (int.TryParse(EventIdTextBox.Text, out int eventId))
        {
            var dbContext = Service.GetDbContext();

            switch (_eventType)
            {
                case "Кино":
                    var cinemaToDelete = Service.GetDbContext().Cinemas.FirstOrDefault(c => c.Id == eventId);
                    if (cinemaToDelete != null)
                        ConfirmAndDelete(cinemaToDelete, dbContext);
                    else
                        NotFoundError();
                    break;
                
                case "Концерт":
                    var concertToDelete = Service.GetDbContext().Concerts.FirstOrDefault(c => c.Id == eventId);
                    if (concertToDelete != null)
                        ConfirmAndDelete(concertToDelete, dbContext);
                    else
                        NotFoundError();
                    break;
                
                default:
                    MessageBox.Show("Неподдерживаемый тип мероприятия.", "Ошибка удаления",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    break;
            }
        }
        else
            MessageBox.Show("Введите корректный ID мероприятия.", "Ошибка удаления",
                MessageBoxButton.OK, MessageBoxImage.Error);
    }
    
    private void ConfirmAndDelete<T>(T entity, DbContext dbContext) where T : class
    {
        var transactionConfirmation = MessageBox.Show(
            "Вы уверены, что хотите удалить это мероприятие?", "Подтверждение удаления",
            MessageBoxButton.YesNo, MessageBoxImage.Warning);

        if (transactionConfirmation == MessageBoxResult.Yes)
        {
            dbContext.Set<T>().Remove(entity);
            dbContext.SaveChanges();
            MessageBox.Show("Мероприятие удалено.");
        }
        else
            MessageBox.Show("Удаление отменено.");
    }
    
    private void NotFoundError()
    {
        MessageBox.Show("Мероприятие с указанным ID не найдено.", "Ошибка удаления",
            MessageBoxButton.OK, MessageBoxImage.Error);
    }
}