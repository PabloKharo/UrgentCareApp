using SQLite;
using OnmpApp.Models;
using OnmpApp.Properties;
using CommunityToolkit.Maui.Core.Extensions;
using Microsoft.Maui.ApplicationModel.Communication;
using System.Runtime.CompilerServices;
using System.Diagnostics;
using OnmpApp.Models.Database;

namespace OnmpApp.Database;

public static class BaseDatabase
{
    public static SQLiteAsyncConnection DB;

    public static async Task Init()
    {
        if (DB is not null)
            return;

        DB = new SQLiteAsyncConnection(Settings.DatabasePath, Settings.DatabaseFlags);

        // Создание таблиц, если они не существуют
        _ = await DB.CreateTableAsync<User>();
        _ = await DB.CreateTableAsync<Card>();
        _ = await DB.CreateTableAsync<FullCard>();
        _ = await DB.CreateTableAsync<Catalog>();

        // TODO: Удалить
        if (await DB.Table<Catalog>().CountAsync() == 0)
        {
            List<Catalog> l = new List<Catalog>
            {
                new Catalog() { Id = 0, Name = "Дыхание Чейна-Стокса", Text = "1" },
                new Catalog() { Id = 0, Name = "Дыхание Биотта", Text = "2" },
                new Catalog() { Id = 0, Name = "Дыхание Куссмауля", Text = "3" },
                new Catalog() { Id = 0, Name = "Положительные симптомы Ровзинга", Text = "4" },
                new Catalog() { Id = 0, Name = "Нистагм", Text = "5" },

            };

            await DB.InsertAllAsync(l);
        }
    }

}

