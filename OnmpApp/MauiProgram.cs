using Microsoft.Extensions.Logging;
using OnmpApp.ViewModels;
using OnmpApp.Views;
using CommunityToolkit.Maui;
using OnmpApp.Database;

namespace OnmpApp;

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
			}).UseMauiCommunityToolkit()
			.RegisterViewModels()
			.RegisterViews();

#if DEBUG
		builder.Logging.AddDebug();
#endif

		_ = BaseDatabase.Init();

        return builder.Build();
	}

	public static MauiAppBuilder RegisterViewModels(this MauiAppBuilder mauiAppBuilder)
	{
        mauiAppBuilder.Services.AddTransient<ViewModels.Authorize.LoginViewModel>();
        mauiAppBuilder.Services.AddTransient<ViewModels.Authorize.RegistrationViewModel>();

        mauiAppBuilder.Services.AddSingleton<ViewModels.MainTabs.SearchTabViewModel>();
        mauiAppBuilder.Services.AddSingleton<ViewModels.MainTabs.CatalogTabViewModel>();
        mauiAppBuilder.Services.AddTransient<ViewModels.Catalog.CatalogTextViewModel>();


        mauiAppBuilder.Services.AddTransient<ViewModels.CardFiller.EditorPreviewCardViewModel>();
        mauiAppBuilder.Services.AddTransient<ViewModels.CardFiller.TemplateFillerViewModel>();


        mauiAppBuilder.Services.AddSingleton<ViewModels.UserSettings.SettingsViewModel>();


        return mauiAppBuilder;
    }

	public static MauiAppBuilder RegisterViews(this MauiAppBuilder mauiAppBuilder)
	{
        mauiAppBuilder.Services.AddTransient<Views.Authorize.LoginPage>();
        mauiAppBuilder.Services.AddTransient<Views.Authorize.RegistrationPage>();

        mauiAppBuilder.Services.AddSingleton<Views.MainTabs.SearchTabPage>();
        mauiAppBuilder.Services.AddSingleton<Views.MainTabs.CatalogTabPage>();
        mauiAppBuilder.Services.AddTransient<Views.Catalog.CatalogTextPage>();


        mauiAppBuilder.Services.AddTransient<Views.CardFiller.EditorPreviewCardPage>();
        mauiAppBuilder.Services.AddTransient<Views.CardFiller.TemplateFillerPage>();


        mauiAppBuilder.Services.AddSingleton<Views.UserSettings.SettingsPage>();

        return mauiAppBuilder;
    }
}
