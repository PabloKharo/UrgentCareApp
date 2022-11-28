using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using UrgentCareApp.Models;
using UrgentCareApp.Services;

namespace UrgentCareApp.ViewModels.Authorize;

public partial class LoginViewModel : ObservableObject
{
    [ObservableProperty]
    bool _savePassword = true;

    [ObservableProperty]
    EmailAddress _email = new(Settings.Email);

    [ObservableProperty]
    string _password = Settings.Password;

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

        if (!await Server.UserExistsAsync(Email.Value, Password))
        {
            IsLoginingIn = false;

            await Task.Delay(50);
            InvalidUserDataOccured = true;
            return;
        }

        Settings.Email = Email.Value;
        if (SavePassword)
            Settings.Password = Password;

        Settings.WasAuthorized = true;

        // TODO: Перейти на следующую страницу

        IsLoginingIn = false;
    }
}
