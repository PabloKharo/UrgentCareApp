using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OnmpApp.Models;

namespace OnmpApp.Converters;

public class CardTypeToStringConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is CardType cardType)
        {
            switch (cardType)
            {
                case CardType.Draft:
                    return Properties.Resources.Draft;
                case CardType.Ready:
                    return Properties.Resources.Ready;
                case CardType.Template:
                    return Properties.Resources.Template;
                case CardType.Archive:
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