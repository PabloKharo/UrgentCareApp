using Microsoft.Extensions.Logging;
using UrgentCareApp.ViewModels;
using UrgentCareApp.Views;
using CommunityToolkit.Maui;

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
			}).UseMauiCommunityToolkit();

#if DEBUG
		builder.Logging.AddDebug();
#endif

        // Добавление страницы аутентификации
        builder.Services.AddTransient<Views.Authorize.LoginPage>();
        builder.Services.AddSingleton<ViewModels.Authorize.LoginViewModel>();

		
        return builder.Build();
	}
}
