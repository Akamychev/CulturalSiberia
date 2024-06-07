using System;
using System.Linq;
using System.Security;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using CulturalSiberiaProject.Models;
using CulturalSiberiaProject.Services;
using CulturalSiberiaProject.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using static CulturalSiberiaProject.Services.Hash;

namespace CulturalSiberiaProject.ViewModels;

public class AuthorizationModel : NotifyProperty
{
    
    public AuthorizationModel()
    {
        AuthorizationCommand = new RelayCommand(AuthorizationButton);
    }
    public ICommand AuthorizationCommand { get; set; }
   

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
    
    private void AuthorizationButton()
    {
        string username = LoginProperty;
        string password = PasswordProperty;

        if (!string.IsNullOrWhiteSpace(username) && !string.IsNullOrWhiteSpace(password))
        {
            var user = Service.GetDbContext().Users.Include(u => u.UserroleNavigation)
                .FirstOrDefault(u => u.Login == username && u.Userpassword == HashPassword(password));
            if (user != null)
            {
                var userRole = user?.UserroleNavigation?.Roles;
                var viewModel = new MainViewModel
                {
                    UserRole = userRole
                };
                
                Service.SetCurrentUser(user);
                
                var NewMainWindowAfterAuthorization = new MainView(viewModel);
                NewMainWindowAfterAuthorization.Show();
                Application.Current.MainWindow.Close(); /////////////////////////////////////////////////////////////////
            }
            else
                MessageBox.Show("Неверный логин или пароль, попробуйте еще раз",
                    "Ошибка авторизации", MessageBoxButton.OK, MessageBoxImage.Error);
            
        }
        else
            MessageBox.Show("Обязательные поля ввода пустые", "Ошибка авторизации", 
                MessageBoxButton.OK, MessageBoxImage.Error);
    }
    
}