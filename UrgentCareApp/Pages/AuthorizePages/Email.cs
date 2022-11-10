using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace UrgentCareApp.Pages.AuthorizePages;

// класс для хранения почтового адреса
public partial class Email : ObservableObject
{
    [ObservableProperty]
    private string value;

    public bool IsEmail()
    {
        return IsEmail(value);
    }

    // Проверка, является ли строка почтовым адресом
    public static bool IsEmail(string str)
    {
        if (string.IsNullOrEmpty(str))
            return false;
        string pattern = "^[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?$";
        return Regex.IsMatch(str.ToLower(), pattern);
    }

}

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

        return Email.IsEmail(email)? border.Stroke : Colors.Red;

    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}