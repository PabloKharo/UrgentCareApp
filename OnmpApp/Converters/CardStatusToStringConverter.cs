using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OnmpApp.Models;

namespace OnmpApp.Converters;

// Конвертер для преобразования типа карты в текстовое поле
public class CardStatusToStringConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is CardStatus cardStatus)
        {
            switch (cardStatus)
            {
                case CardStatus.Draft:
                    return Properties.Resources.Draft;
                case CardStatus.Ready:
                    return Properties.Resources.Ready;
                case CardStatus.Template:
                    return Properties.Resources.Template;
                case CardStatus.Archive:
                    return Properties.Resources.Archive;
                default:
                    return Properties.Resources.UnknownType;
            }
        }

        return Properties.Resources.UnknownType;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}