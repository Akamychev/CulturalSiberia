using System.Linq;
using System.Windows;
using CulturalSiberiaProject.Models;
using CulturalSiberiaProject.Services;
using static CulturalSiberiaProject.Services.Hash;

namespace CulturalSiberiaProject.Views.EmployeeAllOperationWindows;

public partial class CreateEmployeeWindow : Window
{
    public CreateEmployeeWindow()
    {
        InitializeComponent();
    }
    
    private void CloseCurrentWindow_click(object sender, RoutedEventArgs e)
    {
        Window.GetWindow(this).Close();
    }
    
    public void AddEmployeeButton(object sender, RoutedEventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(FirstName.Text)
            && !string.IsNullOrWhiteSpace(LastName.Text)
            && !string.IsNullOrWhiteSpace(Login.Text)
            && !string.IsNullOrWhiteSpace(Password.Text))
        {
            var newEmployee = new User();
            if (Service.GetDbContext().Users.FirstOrDefault(u => u.Login == Login.Text) == null)
            {
                newEmployee.Fname = FirstName.Text;
                newEmployee.Lname = LastName.Text;
                newEmployee.Mname = MiddleName.Text;
                newEmployee.Email = Email.Text;
                newEmployee.Login = Login.Text;
                newEmployee.Userrole = "Employee";
                newEmployee.Userpassword = HashPassword(Password.Text);
                Service.GetDbContext().Users.Add(newEmployee);
                Service.GetDbContext().SaveChanges();
            }
            else
                MessageBox.Show("Сотрудник с таким логином уже есть", "Ошибка добавления", 
                    MessageBoxButton.OK, MessageBoxImage.Error);

            MessageBox.Show("Сотрудник добавлен");
        }
        else
            MessageBox.Show("Не все обязательные поля заполнены", "Ошибка добавления", 
                MessageBoxButton.OK, MessageBoxImage.Error);
    }
}