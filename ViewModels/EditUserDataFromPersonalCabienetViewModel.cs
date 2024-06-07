using System;
using System.Windows;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CulturalSiberiaProject.Models;
using CulturalSiberiaProject.Services;

namespace CulturalSiberiaProject.ViewModels;

public class EditUserDataFromPersonalCabienetViewModel : ObservableObject
{
    public ICommand UpdateCommand { get; }

    public EditUserDataFromPersonalCabienetViewModel(User user)
    {
        CurrentUser = user;
        UpdateCommand = new RelayCommand(SaveChanges);
    }
    
    private User _currentUser;
    public User CurrentUser
    {
        get => _currentUser;
        set => SetProperty(ref _currentUser, value);
    }

    private void SaveChanges()
    {
        // Сохранение изменений в базе данных
        try
        {
            var dbContext = Service.GetDbContext();
            dbContext.Users.Update(CurrentUser);
            dbContext.SaveChanges();
            MessageBox.Show("Изменения сохранены");
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка при сохранении изменений: {ex.Message}");
        }
    }
}