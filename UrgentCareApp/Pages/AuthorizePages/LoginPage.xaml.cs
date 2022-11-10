using Microsoft.Maui.ApplicationModel.Communication;
using System.Collections.Generic;
using UrgentCareServer.Data;
using UrgentCareServer.Models;

namespace UrgentCareApp.Pages.AuthorizePages;

public partial class LoginPage : ContentPage
{
    // TODO: добавить поле "сохранить пароль"
    public bool SavePassword { get; set; } = true;
    public Email Email { get; set; } = new();
    public string Password { get; set; }

    public LoginPage()
    {
        InitializeComponent();

        Email.Value = Preferences.Email;
        Password = Preferences.Password;

        BindingContext = this;
    }
    private void InfoButton_Clicked(object sender, EventArgs e)
    {
        // TODO: перейти на страницу информации приложения
    }
    private void RestorePasswordLabel_Tapped(object sender, TappedEventArgs e)
    {
        // TODO: перети на страницу восстановления пароля
    }

    private async void LoginButton_Clicked(object sender, EventArgs e)
    {
        if (!Email.IsEmail() || string.IsNullOrWhiteSpace(Password))
            return;

        if (!await UserControl.UserExistsAsync(Email.Value, Password))
            return;

        Preferences.Email = Email.Value;
        if (SavePassword)
            Preferences.Password = Password;

        // TODO: Перейти на следующую страницу

    }

    private void RegistrationLabel_Tapped(object sender, TappedEventArgs e)
    {
        // TODO: перейти на страницу регистрации в приложении
    }
}