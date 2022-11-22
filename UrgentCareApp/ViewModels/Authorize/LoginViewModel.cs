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
        // Если ранее был успешный вход, попробовать войти со старыми данными
        if (Preferences.WasAuthorized)
            Login();
    }

    public IAsyncRelayCommand NavigateToInformationPageCommand { get; }
    async void NavigateToInformationPage()
    {
        // TODO: Перейти на страницу информации приложения
    }

    public IAsyncRelayCommand NavigateToRestoringPasswordPageCommand { get; }
    async void NavigateToRestoringPasswordPage()
    {
        // TODO: Перейти на страницу восстановления пароля
    }
    public IAsyncRelayCommand NavigateToRegistrationPageCommand { get; }
    async void NavigateToRegistrationPage()
    {
        // TODO: Перейти на страницу регистрации в приложении
    }

    public IAsyncRelayCommand LoginCommand { get; }
    async void Login()
    {
        // Проверка, что введены правильные данные
        if (!Email.IsEmail() || string.IsNullOrWhiteSpace(Password))
            return;

        // TODO: включить ActivityIndicator
        if (!await Server.UserExistsAsync(Email.Value, Password))
            // TODO: Высветить ошибку
            return;

        Preferences.Email = Email.Value;
        if (SavePassword)
            Preferences.Password = Password;

        // TODO: Перейти на следующую страницу

    }
}
