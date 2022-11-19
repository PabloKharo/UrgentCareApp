using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using UrgentCareApp.Models;
using UrgentCareServer.Data;

namespace UrgentCareApp.ViewModels.Authorize;

public partial class LoginViewModel : ObservableObject
{
    [ObservableProperty]
    bool _savePassword = true;

    [ObservableProperty]
    EmailAddress _email = new(Preferences.Email);

    [ObservableProperty]
    string _password = Preferences.Password;


    public LoginViewModel()
    {
        // Если ранее был успешный вход , попробовать войти со старыми данными
        if (!Preferences.WasAuthorized)
            return;
        Login();
    }

    [RelayCommand]
    async void NavigateToInformationPage()
    {
        // TODO: Перейти на страницу информации приложения
    }

    [RelayCommand]
    async void NavigateToRestoringPasswordPage()
    {
        // TODO: Перейти на страницу восстановления пароля
    }

    [RelayCommand]
    async void NavigateToRegistrationPage()
    {
        // TODO: Перейти на страницу регистрации в приложении
    }

    [RelayCommand]
    async void Login()
    {
        if (!Email.IsEmail() || string.IsNullOrWhiteSpace(Password))
            return;

        if (!await Server.UserExistsAsync(Email.Value, Password))
            return;

        Preferences.Email = Email.Value;
        if (SavePassword)
            Preferences.Password = Password;

        // TODO: Перейти на следующую страницу

    }
}
