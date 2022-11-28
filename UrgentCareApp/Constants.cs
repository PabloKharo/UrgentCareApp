using SQLite;

namespace UrgentCareApp;

public static class Constants
{
    // Название БД
    public const string DatabaseFilename = "urgent_care.db3";

    // Путь к БД
    public static string DatabasePath => GetPath();

    private static string GetPath()
    {
        try
        {
            return Path.Combine(FileSystem.AppDataDirectory, DatabaseFilename);
        }
        catch
        {
            // Необходимо для тестирования
            return Path.Combine(@"C:\UrgentCareApp", DatabaseFilename);
        }
    }

    // Флаги для инициализации БД
    public const SQLiteOpenFlags DatabaseFlags =
        // Открыть БД на чтение и запись
        SQLiteOpenFlags.ReadWrite |
        // Создать БД, если не существует
        SQLiteOpenFlags.Create |
        // Включить мультипоточный доступ к БД
        SQLiteOpenFlags.SharedCache;

}