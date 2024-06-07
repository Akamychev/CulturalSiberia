using System;
using System.Linq;
using System.Windows;
using CulturalSiberiaProject.Models;
using CulturalSiberiaProject.Services;
using static CulturalSiberiaProject.Services.Hash;

namespace CulturalSiberiaProject.Views.EmployeeAllOperationWindows;

public partial class UpdateEmployeeWindow : Window
{
    private User _employee;
    
    public UpdateEmployeeWindow()
    {
        InitializeComponent();
    }
    
    private void CloseCurrentWindow_Click(object sender, RoutedEventArgs e)
    {
        Window.GetWindow(this).Close();
    }
    
    private void LoadEmployeeDataButton_Click(object sender, RoutedEventArgs e)
    {
        int employeeId;
        if (int.TryParse(EmployeeId.Text, out employeeId))
        {
            _employee = Service.GetDbContext().Users.FirstOrDefault(c => c.Id == employeeId);
            if (_employee != null)
            {
                FirstName.Text = _employee.Fname;
                LastName.Text = _employee.Lname;
                MiddleName.Text = _employee.Mname;
                Email.Text = _employee.Email;
                Login.Text = _employee.Login;
                Password.Text = _employee.Userpassword;
            }
            else
                MessageBox.Show("Сотрудник с указанным ID не найден.");
        }
        else
            MessageBox.Show("Введите корректный ID сотрудника.");
    }
    
    private void UpdateCinemaButton_Click(object sender, RoutedEventArgs e)
    {
        if (_employee != null)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(FirstName.Text)
                    && !string.IsNullOrWhiteSpace(LastName.Text)
                    && !string.IsNullOrWhiteSpace(Login.Text)
                    && !string.IsNullOrWhiteSpace(Password.Text))
                {
                    _employee.Fname = FirstName.Text;
                    _employee.Lname = LastName.Text;
                    _employee.Mname = MiddleName.Text;
                    _employee.Email = Email.Text;
                    _employee.Login = Login.Text;
                    _employee.Userpassword = HashPassword(Password.Text);

                    Service.GetDbContext().SaveChanges();

                    MessageBox.Show("Данные о сотруднике обновлены.");
                }
                else
                    MessageBox.Show("Обязательные поля не заполнены", "Ошибка обновления сотрудника",
                        MessageBoxButton.OK,MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при обновлении данных: {ex.Message}");
            }
        }
        else
            MessageBox.Show("Сначала загрузите данные о сотруднике.");
    }
}