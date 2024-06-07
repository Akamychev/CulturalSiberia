using System.Collections.Generic;
using System.Linq;
using System.Windows;
using CulturalSiberiaProject.Models;
using CulturalSiberiaProject.Services;
using CulturalSiberiaProject.ViewModels;

namespace CulturalSiberiaProject.Views;

public partial class TicketView : Window
{
    public TicketView()
    {
        InitializeComponent();
        DataContext = new TicketViewModel();
    }

    private void CloseCurrentWindow_Click(object sender, RoutedEventArgs e)
    {
        Window.GetWindow(this).Close();
    }

    private void TicketPdfReport_Click(object sender, RoutedEventArgs e)
    {
        List<Ticket> data = Service.GetDbContext().Tickets.ToList();
        PdfReportForTickets.PrintPdfReport(data);
    }
}