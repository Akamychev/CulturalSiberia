using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using CulturalSiberiaProject.Models;

namespace CulturalSiberiaProject.Services;

public class EventTypeToVisibilityConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is Event @event)
        {
            if (@event.Cinema != null && parameter.ToString() == "Cinema")
            {
                return Visibility.Visible;
            }
            if (@event.Concert != null && parameter.ToString() == "Concert")
            {
                return Visibility.Visible;
            }
        }
        return Visibility.Collapsed;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}