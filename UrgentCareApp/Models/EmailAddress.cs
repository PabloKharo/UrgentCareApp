using CommunityToolkit.Mvvm.ComponentModel;
using System.Text.RegularExpressions;

namespace UrgentCareApp.Models;

// Класс для хранения почтового адреса
public partial class EmailAddress : ObservableObject
{
    [ObservableProperty]
    private string _value;

    public EmailAddress() { }
    public EmailAddress(string value) => Value = value;

    public bool IsEmail() => IsEmail(Value);

    // Проверка, является ли строка почтовым адресом
    public static bool IsEmail(string str)
    {
        if (string.IsNullOrEmpty(str))
            return false;
        string pattern = "^[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?$";
        return Regex.IsMatch(str.ToLower(), pattern);
    }

}
