using System;
using System.Globalization;
using System.Windows.Data;

namespace CulturalSiberiaProject.Services;

public class BooleanToFavoriteTextConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is bool isFavorite)
        {
            return isFavorite ? "Удалить из избранного" : "В избранное";
        }
        return "В избранное";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}