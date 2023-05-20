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
        Properties.Settings.WasAuthorized = false;
        Properties.Settings.Password = "";
        Properties.Settings.Token = "";
        await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
    }
}
