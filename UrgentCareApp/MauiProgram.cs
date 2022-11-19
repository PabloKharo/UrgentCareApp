using Microsoft.Extensions.Logging;
using UrgentCareApp.ViewModels;
using UrgentCareApp.Pages;

namespace UrgentCareApp;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

#if DEBUG
		builder.Logging.AddDebug();
#endif

        builder.Services.AddTransient<Pages.Authorize.LoginPage>();
        builder.Services.AddSingleton<ViewModels.Authorize.LoginViewModel>();


        return builder.Build();
	}
}
