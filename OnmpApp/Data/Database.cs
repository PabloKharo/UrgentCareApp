using SQLite;
using OnmpApp.Models;
using OnmpApp.Properties;
using CommunityToolkit.Maui.Core.Extensions;
using Microsoft.Maui.ApplicationModel.Communication;
using System.Runtime.CompilerServices;
using System.Diagnostics;

namespace OnmpApp.Data;

public static partial class Database
{
    private static SQLiteAsyncConnection db;

    public static async Task Init()
    {
        if (db is not null)
            return;

        db = new SQLiteAsyncConnection(Settings.DatabasePath, Settings.DatabaseFlags);

        // Создание таблиц, если они не существуют
        var user = await db.CreateTableAsync<User>();
        var card = await db.CreateTableAsync<Card>();

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
        string rd = CardStatus.Ready.ToString();
        string tm = CardStatus.Template.ToString();
        string df = CardStatus.Draft.ToString();
        string av = CardStatus.Archive.ToString();

        var res = await db.Table<Card>().Where(el => el.UserId == Settings.UserId && el.Name.Contains(searchText)).ToListAsync();
        res = res.Where(el => (draftChecked ? el._status == df : false) ||
                    (readyChecked ? el._status == rd : false) ||
                    (templateChecked ? el._status == tm : false) ||
                    (archiveChecked ? el._status == av : false)).ToList();
        return res;
    }

    public static async Task<bool> CardCreate(Card card)
    {
        _ = await db.InsertAsync(card);
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


    #region Template


    #endregion
}
