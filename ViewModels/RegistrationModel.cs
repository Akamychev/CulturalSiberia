using System.Linq;
using System.Windows;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using CulturalSiberiaProject.Models;
using CulturalSiberiaProject.Services;
using CulturalSiberiaProject.Views;
using static CulturalSiberiaProject.Services.Hash;

namespace CulturalSiberiaProject.ViewModels;

public class RegistrationModel : NotifyProperty
{
    public RegistrationModel()
    {
        RegistrationButtonCommand = new RelayCommand(RegistrationButton);
    }

    public ICommand RegistrationButtonCommand { get; set; }
    
    private string _fnameProperty;
    public string FNameProperty
    {
        get { return _fnameProperty; }
        set
        {
            if (_fnameProperty != value)
            {
                _fnameProperty = value;
                OnPropertyChanged(nameof(FNameProperty));
            }
        }
    }
    
    private string _lnameProperty;
    public string LNameProperty
    {
        get { return _lnameProperty; }
        set
        {
            if (_lnameProperty != value)
            {
                _lnameProperty = value;
                OnPropertyChanged(nameof(LNameProperty));
            }
        }
    }
    
    private string _mnameProperty;
    public string MNameProperty
    {
        get { return _mnameProperty; }
        set
        {
            if (_mnameProperty != value)
            {
                _mnameProperty = value;
                OnPropertyChanged(nameof(MNameProperty));
            }
        }
    }

    private string _emailProperty;
    public string EmailProperty
    {
        get { return _emailProperty; }
        set
        {
            if (_emailProperty != value)
            {
                _emailProperty = value;
                OnPropertyChanged(nameof(EmailProperty));
            }
        }
    }

    private string _loginProperty;
    public string LoginProperty
    {
        get { return _loginProperty; }
        set
        {
            if (_loginProperty != value)
            {
                _loginProperty = value;
                OnPropertyChanged(nameof(LoginProperty));
            }
        }
    }

    private string _passwordProperty;
    public string PasswordProperty
    {
        get { return _passwordProperty; }
        set
        {
            if (_passwordProperty != value)
            {
                _passwordProperty = value;
                OnPropertyChanged(nameof(PasswordProperty));
            }
        }
    }

    public void RegistrationButton()
    {
        if (!string.IsNullOrWhiteSpace(FNameProperty)
            && !string.IsNullOrWhiteSpace(LNameProperty)
            && !string.IsNullOrWhiteSpace(LoginProperty)
            && !string.IsNullOrWhiteSpace(PasswordProperty))
        {
            var user = new User();
            if (Service.GetDbContext().Users.FirstOrDefault(u => u.Login == LoginProperty) == null)
            {
                user.Fname = FNameProperty;
                user.Lname = LNameProperty;
                user.Mname = MNameProperty;
                user.Email = EmailProperty;
                user.Login = LoginProperty;
                user.Userrole = "Client";
                user.Userpassword = HashPassword(PasswordProperty);
                Service.GetDbContext().Users.Add(user);
                Service.GetDbContext().SaveChanges();
            }
            else
                MessageBox.Show("Пользователь с таким логином уже есть, попробуйте ввести другой логин",
                    "Ошибка регистрации", MessageBoxButton.OK, MessageBoxImage.Error);

            MessageBox.Show("Регистрация прошла успешно!", "Успешная регистрация",
                MessageBoxButton.OK, MessageBoxImage.Asterisk);
            
            //
            //
        }
        else
            MessageBox.Show("Не все обязательные поля заполнены", "Ошибка регистрации", 
                MessageBoxButton.OK, MessageBoxImage.Error);
    }
}