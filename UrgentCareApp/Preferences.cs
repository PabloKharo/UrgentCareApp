namespace UrgentCareApp;

public static class Preferences
{
    public static string Email
    {
        get => Microsoft.Maui.Storage.Preferences.Default.Get(nameof(Email), string.Empty);
        set => Microsoft.Maui.Storage.Preferences.Default.Set(nameof(Email), value);
    }

    public static string Password
    {
        get => Microsoft.Maui.Storage.Preferences.Default.Get(nameof(Password), string.Empty);
        set => Microsoft.Maui.Storage.Preferences.Default.Set(nameof(Password), value);
    }

    public static bool WasAuthorized
    {
        get => Microsoft.Maui.Storage.Preferences.Default.Get(nameof(WasAuthorized), false); 
        set => Microsoft.Maui.Storage.Preferences.Default.Set(nameof(WasAuthorized), value);
    }

}