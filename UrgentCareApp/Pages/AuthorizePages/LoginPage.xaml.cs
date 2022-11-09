namespace UrgentCareApp.Pages.AuthorizePages;

public partial class LoginPage : ContentPage
{
    public Email Email { get; set; } = new();
    public string Password { get; set; }

    public LoginPage()
    {
        InitializeComponent();

        Email.Value = Preferences.Default.Get(nameof(Email), string.Empty);
        Password = Preferences.Default.Get(nameof(Password), string.Empty);

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


        Preferences.Default.Set(nameof(Email), Email.Value);
        Preferences.Default.Set(nameof(Password), Password);
    }

    private void RegistrationLabel_Tapped(object sender, TappedEventArgs e)
    {

    }
}