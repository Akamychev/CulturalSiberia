using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CulturalSiberiaProject.Models;
using CulturalSiberiaProject.Services;
using CulturalSiberiaProject.Views;

namespace CulturalSiberiaProject.ViewModels;

public class PersonalCabienetViewModel : ObservableObject 
{
    private string _firstName;
    private string _lastName;
    private string _email;
    
    public PersonalCabienetViewModel()
    {
        LoadUserProfile();
    }

    public string FirstName
    {
        get => _firstName;
        set => SetProperty(ref _firstName, value);
    }

    public string LastName
    {
        get => _lastName;
        set => SetProperty(ref _lastName, value);
    }

    public string Email
    {
        get => _email;
        set => SetProperty(ref _email, value);
    }

    public ICommand EditProfileCommand { get; }

    private void LoadUserProfile()
    {
        // Загрузка данных профиля пользователя и истории мероприятий
        var user = Service.GetCurrentUser();
        FirstName = user.Fname;
        LastName = user.Lname;
        if (user.Email != null)
        {
            Email = user.Email;
        }
        else
            Email = "Email отсутствует";
    }
}