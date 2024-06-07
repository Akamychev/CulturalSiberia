using System;
using System.Collections.Generic;
using System.Windows;
using CulturalSiberiaProject.Models;
using CulturalSiberiaProject.Services;
using CulturalSiberiaProject.ViewModels;

namespace CulturalSiberiaProject.Views;

public partial class EventDetailsView : Window
{
    private static List<Ticket> _tickets = new List<Ticket>();
    public EventDetailsView()
    {
        InitializeComponent();
    }

    private void CloseCurrentWindow_Click(object sender, RoutedEventArgs e)
    {
        Window.GetWindow(this).Close();
    }
    
    private void BuyTicket_Click(object sender, RoutedEventArgs e)
    {
        var viewModel = (EventDetailsViewModel)DataContext;
        string eventName;
        DateOnly? eventDate;

        if (viewModel.Cinema != null)
        {
            eventName = viewModel.Cinema.Nameing;
            eventDate = viewModel.Cinema.Realisedate;
        }
        else if (viewModel.Concert != null)
        {
            eventName = viewModel.Concert.Programofconcert;
            eventDate = viewModel.Concert.Concertdate;
        }
        else
        {
            MessageBox.Show("Не удалось определить тип мероприятия.", "Ошибка", 
                MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        var ticket = new Ticket
        {
            Eventname = eventName,
            Eventdate = eventDate,
            Price = 100
        };
        
        Service.GetDbContext().Tickets.Add(ticket);
        Service.GetDbContext().SaveChanges();

        MessageBox.Show("Билет приобретен", "Покупка", 
            MessageBoxButton.OK, MessageBoxImage.Information);
    }
}