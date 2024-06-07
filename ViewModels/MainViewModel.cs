using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using CulturalSiberiaProject.Models;
using CulturalSiberiaProject.Services;
using CulturalSiberiaProject.Views;
using Microsoft.EntityFrameworkCore;

namespace CulturalSiberiaProject.ViewModels;

public partial class MainViewModel : NotifyProperty
{
    public MainViewModel()
    {
        Events = new ObservableCollection<Event>();
        LoadEvents();
        RefreshCommand = new RelayCommand(LoadEvents);
        ToggleFavoriteCommand = new RelayCommand<Event>(ToggleFavorite);
        OpenEventDetailsCommand = new RelayCommand<Event>(OpenEventDetails);
    }
    
    private string _userRole;
    public string UserRole
    {
        get => _userRole;
        set
        {
            _userRole = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(IsAdminOrStaff));
        }
    }
    public bool IsAdminOrStaff => UserRole == "Admin" || UserRole == "Employee";
    
    private ObservableCollection<Event> _events;
    public ObservableCollection<Event> Events
    {
        get => _events;
        set
        {
            _events = value;
            OnPropertyChanged();
        }
    }

    private bool _filterCinema;
    public bool FilterCinema
    {
        get => _filterCinema;
        set
        {
            _filterCinema = value;
            OnPropertyChanged();
            FilterEvents();
        }
    }

    private bool _filterConcert;
    public bool FilterConcert
    {
        get => _filterConcert;
        set
        {
            _filterConcert = value;
            OnPropertyChanged();
            FilterEvents();
        }
    }
    
    private string _searchQuery;
    public string SearchQuery
    {
        get => _searchQuery;
        set
        {
            _searchQuery = value;
            OnPropertyChanged();
            FilterEvents();
        }
    }
    
    public ICommand RefreshCommand { get; set; }
    public void LoadEvents()
    {
        var query = Service.GetDbContext().Events
            .Include(e => e.Cinema)
            .Include(e => e.Concert);
        
        var allEvents = query.ToList();
        
        Events.Clear();
        
        foreach (var eventDataItem in allEvents)
        {
            Events.Add(eventDataItem);
        }
    }
    
    private void FilterEvents()
    {
        var allEvents = Service.GetDbContext().Events
            .Include(e => e.Cinema)
            .Include(e => e.Concert)
            .ToList();

        var filteredEvents = allEvents.Where(e =>
            (!FilterCinema || e.Cinema != null) &&
            (!FilterConcert || e.Concert != null) &&
            (string.IsNullOrWhiteSpace(SearchQuery) ||
             (e.Cinema != null && e.Cinema.Nameing.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase)) ||
             (e.Concert != null && e.Concert.Programofconcert.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase)))
        ).ToList();

        Events = new ObservableCollection<Event>(filteredEvents);
    }
    
    public ICommand ToggleFavoriteCommand { get; set; }
    
    private void ToggleFavorite(Event selectedEvent)
    {
        if (selectedEvent != null)
        {
            selectedEvent.Isfavorite = !selectedEvent.Isfavorite;
            Service.GetDbContext().Events.Update(selectedEvent);
            Service.GetDbContext().SaveChanges();
            OnPropertyChanged(nameof(Events));
        }
    }
    
    public ICommand OpenEventDetailsCommand { get; }

    private void OpenEventDetails(Event selectedEvent)
    {
        if (selectedEvent != null)
        {
            var eventDetailsView = new EventDetailsView()
            {
                DataContext = new EventDetailsViewModel(selectedEvent)
            };
            eventDetailsView.Show();
        }
    }
}