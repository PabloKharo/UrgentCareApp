using SQLite;
using OnmpApp.Models;
using OnmpApp.Properties;
using CommunityToolkit.Maui.Core.Extensions;
using Microsoft.Maui.ApplicationModel.Communication;
using System.Runtime.CompilerServices;
using System.Diagnostics;
using OnmpApp.Models.Database;

namespace OnmpApp.Services.Database;

public static partial class DatabaseService
{
    private static SQLiteAsyncConnection db;

    public static async Task Init()
    {
        if (db is not null)
            return;

        db = new SQLiteAsyncConnection(Settings.DatabasePath, Settings.DatabaseFlags);

        // Создание таблиц, если они не существуют
        _ = await db.CreateTableAsync<User>();
        _ = await db.CreateTableAsync<Card>();
        _ = await db.CreateTableAsync<FullCard>();
        _ = await db.CreateTableAsync<Catalog>();


        if (await db.Table<Catalog>().CountAsync() == 0)
        {
            List<Catalog> l = new List<Catalog>
            {
                new Catalog() { Id = 0, Name = "Дыхание Чейна-Стокса", Text = "1" },
                new Catalog() { Id = 0, Name = "Дыхание Биотта", Text = "2" },
                new Catalog() { Id = 0, Name = "Дыхание Куссмауля", Text = "3" },
                new Catalog() { Id = 0, Name = "Положительные симптомы Ровзинга", Text = "4" },
                new Catalog() { Id = 0, Name = "Нистагм", Text = "5" },

            };

            await db.InsertAllAsync(l);
        }
    }

    #region UserTable

    public static async Task<bool> UserEmailExists(string email)
    {
        var cnt = await db.Table<User>().Where(x => x.Email == email).CountAsync();
        if (cnt > 0)
            return true;

        return false;
    }

    public static async Task<int> UserGetId(string email)
    {
        var cnt = await db.Table<User>().Where(x => x.Email == email).FirstOrDefaultAsync();
        return cnt.Id;
    }

    public static async Task<bool> UserCreate(string email)
    {
        if (await UserEmailExists(email))
            return false;

        _ = await db.InsertAsync(new User(email));
        return true;
    }


    #endregion

    #region Card

    public static async Task<List<Card>> CardsSearch(string searchText, bool draftChecked, bool readyChecked,
                                        bool templateChecked, bool archiveChecked, int skip, int take)
    {
        var res = await db.Table<Card>().Where(el => el.UserId == Settings.UserId && el.Name.Contains(searchText)).ToListAsync();
        res = res.Where(el => (draftChecked && el.Status == CardStatus.Draft) ||
                    (readyChecked && el.Status == CardStatus.Ready) ||
                    (templateChecked && el.Status == CardStatus.Template) ||
                    (archiveChecked && el.Status == CardStatus.Archive)).ToList();
        return res;
    }

    public static async Task<bool> CardCreate(Card card)
    {
        _ = await db.InsertAsync(card);
        return true;
    }
    public static async Task<bool> CardUpdate(Card card)
    {
        _ = await db.UpdateAsync(card);
        return true;
    }

    public static async Task<bool> CardRemove(Card card)
    {
        _ = await db.DeleteAsync<Card>(card.Id);
        return true;
    }

    public static async Task<Card> CardGet(int id)
    {
        var card = await db.GetAsync<Card>(id);
        return card;
    }

    public static async Task<string> CardGetLastOrder()
    {
        var card = await db.Table<Card>().Where(el => el.UserId == Settings.UserId).OrderByDescending(el => el.Date).FirstOrDefaultAsync();
        if (card == null)
            return "";
        return card.Order;
    }

    #endregion

    #region FullCard

    public static async Task<bool> FullCardCreate(FullCard card)
    {
        _ = await db.InsertAsync(card);
        return true;
    }
    public static async Task<bool> FullCardUpdate(FullCard card)
    {
        _ = await db.UpdateAsync(card);
        return true;
    }

    public static async Task<bool> FullCardRemove(FullCard card)
    {
        _ = await db.DeleteAsync<FullCard>(card.Id);
        return true;
    }

    public static async Task<FullCard> FullCardGet(int id)
    {
        try
        {
            var card = await db.GetAsync<FullCard>(id);
            return card;
        }
        catch (Exception)
        {
            FullCard card = new()
            {
                Id = id
            };
            await db.InsertAsync(card);
            return card;
        }
    }

    #endregion


    #region Catalog

    public static async Task<List<string>> CatalogSearch(string search, int skip = 0, int take = 0)
    {

        try
        {
            string query = "SELECT Name FROM Catalogs WHERE Name LIKE ?";
            var res = await db.QueryAsync<CatalogName>(query, $"%{search}%");
            return res.Select(x => x.Name).ToList();
        }
        catch (Exception)
        {

            return null;
        }
    }

    public static async Task<Catalog> CatalogGet(string name)
    {
        try
        {
            var card = await db.Table<Catalog>().Where(el => el.Name == name).FirstOrDefaultAsync();
            return card;
        }
        catch (Exception)
        {
            
            return null;
        }
    }

    #endregion
}

