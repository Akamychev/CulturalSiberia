using System;
using CulturalSiberiaProject.ViewModels;
using CulturalSiberiaProject.Views;

namespace CulturalSiberiaProject;

public partial class App
{
    public App()
    {
        var view = new Authorization()
        {
            DataContext = Activator.CreateInstance<AuthorizationModel>()
        };

        view.Show();
    }
}