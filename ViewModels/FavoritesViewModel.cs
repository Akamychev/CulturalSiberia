using System;
using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CulturalSiberiaProject.Models;
using CulturalSiberiaProject.Services;
using Microsoft.EntityFrameworkCore;

namespace CulturalSiberiaProject.ViewModels;

public class FavoritesViewModel : ObservableObject
{
    public FavoritesViewModel()
    {
        LoadFavoriteEvents();
    }
    
    private ObservableCollection<Event> _favoriteEvents;
    public ObservableCollection<Event> FavoriteEvents
    {
        get => _favoriteEvents;
        set
        {
            _favoriteEvents = value;
            OnPropertyChanged();
        }
    }
    
    private void LoadFavoriteEvents()
    {
        try
        {
            var context = Service.GetDbContext();

            var allEvents = context.Events
                .Where(e => e.Isfavorite)
                .Include(e => e.Cinema)
                .Include(e => e.Concert)
                .ToList();
            
            foreach (var evt in allEvents)
            {
                Console.WriteLine($"Event ID: {evt.Id}, Cinema Date: {evt.Cinema?.Realisedate}, Concert Date: {evt.Concert?.Concertdate}");
            }
            
            FavoriteEvents = new ObservableCollection<Event>(allEvents);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception in LoadFavoriteEvents: {ex.Message}");
        }
    }
}