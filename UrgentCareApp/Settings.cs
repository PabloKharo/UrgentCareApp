namespace UrgentCareApp;

public static class Settings
{
    // Почта пользователя
    public static string Email
    {
        get => Preferences.Get(nameof(Email), string.Empty);
        set => Preferences.Set(nameof(Email), value);
    }

    // Пароль пользователя
    public static string Password
    {
        get => Preferences.Get(nameof(Password), string.Empty);
        set => Preferences.Set(nameof(Password), value);
    }

    // Статус: был ли пользователь успешно авторизован в приложение
    public static bool WasAuthorized
    {
        get => Preferences.Get(nameof(WasAuthorized), false); 
        set => Preferences.Set(nameof(WasAuthorized), value);
    }

}