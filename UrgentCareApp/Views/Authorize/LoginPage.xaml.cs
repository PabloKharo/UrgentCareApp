using UrgentCareApp.Controls;
using UrgentCareApp.ViewModels.Authorize;

namespace UrgentCareApp.Views.Authorize;

public partial class LoginPage : MasterContentPage
{
    public LoginPage(LoginViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}