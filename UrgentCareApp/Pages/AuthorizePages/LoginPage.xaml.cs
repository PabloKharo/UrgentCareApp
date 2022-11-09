namespace UrgentCareApp.Pages.AuthorizePages;

public partial class LoginPage : ContentPage
{
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

    }
    private void RestorePasswordLabel_Tapped(object sender, TappedEventArgs e)
    {

    }

    private void LoginButton_Clicked(object sender, EventArgs e)
    {
        if (!Email.IsEmail() || string.IsNullOrWhiteSpace(Password))
            return;
    }

    private void RegistrationLabel_Tapped(object sender, TappedEventArgs e)
    {

    }
}