using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using UrgentCareApp.Models;
using UrgentCareApp.Helpers;
using UrgentCareApp.Services.Authorize;

namespace UrgentCareApp.ViewModels.Authorize;

public partial class LoginViewModel : ObservableObject
{
    [ObservableProperty]
    string _email = new(Settings.Email);

    [ObservableProperty]
    string _password = Settings.Password;

    [ObservableProperty]
    bool _savePassword = true;

    [ObservableProperty] // Поле состояния запроса к серверу
    [NotifyPropertyChangedFor(nameof(IsNotLoginingIn))]
    bool _isLoginingIn = false;
    public bool IsNotLoginingIn { get => !IsLoginingIn;}

    [ObservableProperty] // Поле состояния ошибки при вводе данных пользователя
    bool _invalidUserDataOccured = false;

    public LoginViewModel()
    {
        // Если ранее был успешный вход, попробовать войти со старыми данными
        if (Settings.WasAuthorized)
            _ = Login();
    }

    [RelayCommand]
    async Task NavigateToInformationPage()
    {
        // TODO: Перейти на страницу информации приложения
    }

    [RelayCommand]
    async Task NavigateToRestoringPasswordPage()
    {
        // TODO: Перейти на страницу восстановления пароля
    }

    [RelayCommand]
    async Task NavigateToRegistrationPage()
    {
        // TODO: Перейти на страницу регистрации в приложении
    }

    [RelayCommand]
    async Task Login()
    {
        InvalidUserDataOccured = false;

        // Проверка, что введены правильные данные
        if (!Email.IsEmail() || string.IsNullOrWhiteSpace(Password))
        {
            InvalidUserDataOccured = true;
            return;
        }

        IsLoginingIn = true;

        // TODO: Убрать задержку
        await Task.Delay(1000);

        LoginService loginService = new();
        string authToken = await loginService.AuthenticateUser(Email, Password);
        if (authToken == string.Empty)
        {
            IsLoginingIn = false;
            InvalidUserDataOccured = true;
            return;
        }

        Settings.AuthToken = authToken;
        Settings.Email = Email;
        if (SavePassword)
            Settings.Password = Password;

        Settings.WasAuthorized = true;

        // TODO: Перейти на следующую страницу

        IsLoginingIn = false;
    }
}
