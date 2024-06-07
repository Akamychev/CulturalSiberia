using System.Collections.Generic;
using CulturalSiberiaProject.Models;

namespace CulturalSiberiaProject.Services;

public class Service
{
    private static CulturalSiberiaContext _bd;
    private static User _currentUser;
    private static List<Ticket> _tickets = new List<Ticket>();

    public static CulturalSiberiaContext GetDbContext()
    {
        if (_bd == null)
            _bd = new CulturalSiberiaContext();

        return _bd;
    }

    public static void SetCurrentUser(User user)
    {
        _currentUser = user;
    }
    
    public static User GetCurrentUser()
    {
        return _currentUser;
    }
    
    public static void AddTicket(Ticket ticket)
    {
        _tickets.Add(ticket);
    }

    public static List<Ticket> GetTickets()
    {
        return _tickets;
    }
}