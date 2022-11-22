using UrgentCareApp.ViewModels.Authorize;

namespace UrgentCareApp.Views.Authorize;

public partial class LoginPage : ContentPage
{
    public LoginPage(LoginViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}