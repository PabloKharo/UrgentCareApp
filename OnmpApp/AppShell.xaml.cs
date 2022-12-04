using OnmpApp.Views.Authorize;

namespace OnmpApp;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		Routing.RegisterRoute(nameof(RegistrationPage), typeof(RegistrationPage));
	}
}
