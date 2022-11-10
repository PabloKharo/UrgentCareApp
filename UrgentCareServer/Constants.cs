using SQLite;

namespace UrgentCareServer;

public static class Constants
{
    public const string DatabaseFilename = "urgent_care.db3";

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
        

    public const SQLite.SQLiteOpenFlags Flags =
        // Открыть БД на чтение и запись
        SQLite.SQLiteOpenFlags.ReadWrite |
        // Создать БД, если не существует
        SQLite.SQLiteOpenFlags.Create |
        // Включить мультипоточный доступ к БД
        SQLite.SQLiteOpenFlags.SharedCache;

}