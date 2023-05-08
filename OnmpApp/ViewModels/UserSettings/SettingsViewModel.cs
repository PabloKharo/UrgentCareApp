using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OnmpApp.Views.Authorize;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnmpApp.ViewModels.UserSettings;

public partial class SettingsViewModel : ObservableObject
{

    [ObservableProperty]
    string _currentVersion = AppInfo.Current.VersionString;

    [RelayCommand] // Выход из программы
    async Task LogOut()
    {

/* Необъединенное слияние из проекта "OnmpApp (net7.0-windows10.0.19041.0)"
До:
        OnmpApp.Settings.WasAuthorized = false;
        OnmpApp.Settings.Password = "";
        OnmpApp.Settings.Token = "";
После:
        Settings.WasAuthorized = false;
        Settings.Password = "";
        Settings.Token = "";
*/
        Properties.Settings.WasAuthorized = false;
        Properties.Settings.Password = "";
        Properties.Settings.Token = "";
        await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
    }
}
