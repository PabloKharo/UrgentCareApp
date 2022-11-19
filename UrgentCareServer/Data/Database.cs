using SQLite;
using UrgentCareServer.Models;

namespace UrgentCareServer.Data;

public static class Database
{
    private static SQLiteAsyncConnection db;
    private static async Task Init()
    {
        if (db is not null)
            return;

        db = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);

        // Создание таблиц, если они не существуют
    }
}
