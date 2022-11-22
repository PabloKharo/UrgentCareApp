using SQLite;
using UrgentCareServer.Models;

namespace UrgentCareServer.Data;


// TODO: Заменить работу с БД на работу с сервером
public static class Server
{
    private static SQLiteAsyncConnection db;
    private static async Task Init()
    {
        if (db is not null)
            return;

        db = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);

        // Создание таблиц, если они не существуют
        var res = await db.CreateTableAsync<User>();
    }

    // Сохранение пользователя (убрать)
    public static async Task<int> SaveUserAsync(User user)
    {
        await Init();
        if(user.Id != 0)
            return await db.UpdateAsync(user);
        else
            return await db.InsertAsync(user);
    }

    // Удаление пользователя (убрать)
    public static async Task<int> DeleteUserAsync(User user)
    {
        await Init();
        return await db.DeleteAsync(user);
    }

    // Проверка, что пользователь существует (убрать)
    public static async Task<bool> UserExistsAsync(string email)
    {
        await Init();
        return await db.Table<User>().Where(user => user.Email == email).CountAsync() > 0;
    }

    // Аутентификация пользователя
    public static async Task<bool> UserExistsAsync(string email, string password)
    {
        await Init();
        return await db.Table<User>().Where(user => user.Email == email && user.Password == password).CountAsync() > 0;
    }
}
