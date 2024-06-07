using System.Collections.Generic;
using System.Linq;
using CulturalSiberiaProject.Models;
using CulturalSiberiaProject.Services;

namespace CulturalSiberiaProject.ViewModels;

public class TicketViewModel : NotifyProperty
{
    public TicketViewModel()
    {
        LoadTickets();
    }
    
    private List<Ticket> tickets;
    public List<Ticket> Tickets
    {
        get => tickets;
        set
        {
            tickets = value;
            OnPropertyChanged();
        }
    }

    private void LoadTickets()
    {
        Tickets = Service.GetDbContext().Tickets.ToList();
    }
}