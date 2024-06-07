using CulturalSiberiaProject.Models;
using CulturalSiberiaProject.Services;

namespace CulturalSiberiaProject.ViewModels;

public class EventDetailsViewModel : NotifyProperty
{
    public EventDetailsViewModel(Event selectedEvent)
    {
        SelectedEvent = selectedEvent;
    }

    private Event _selectedEvent;
    public Event SelectedEvent
    {
        get => _selectedEvent;
        set
        {
            _selectedEvent = value;
            OnPropertyChanged();
        }
    }

    public Cinema Cinema => SelectedEvent?.Cinema;
    public Concert Concert => SelectedEvent?.Concert;
}
