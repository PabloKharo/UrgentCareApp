using System.Globalization;
using UrgentCareApp.Models;

namespace UrgentCareApp.Converters
{
    // Converter для возврата красного цвета границы, если почта неверная
    public class EmailBorderStrokeColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string email = value as string;
            Border border = new Border();

            // Если поле пустое, то красивее, когда полоса черная
            if (string.IsNullOrEmpty(email))
                return border.Stroke;

            return EmailAddress.IsEmail(email) ? border.Stroke : Colors.Red;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
