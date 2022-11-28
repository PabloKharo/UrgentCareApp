using SQLite;
using UrgentCareApp.Models;

namespace UrgentCareApp.Data;

public static class Database
{
    private static SQLiteAsyncConnection db;
    private static async Task Init()
    {
        if (db is not null)
            return;

        db = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.DatabaseFlags);

        // Создание таблиц, если они не существуют
    }
}
