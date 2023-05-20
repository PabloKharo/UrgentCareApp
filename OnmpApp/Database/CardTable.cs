using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnmpApp.Models.Database;
using OnmpApp.Properties;

namespace OnmpApp.Database;

public static class CardTable
{
    public static async Task<List<Card>> Search(string searchText, bool draftChecked, bool readyChecked,
        bool templateChecked, bool archiveChecked, int skip, int take)
    {
        var res = await BaseDatabase.DB.Table<Card>().Where(el => el.UserId == Settings.UserId && el.Name.Contains(searchText)).ToListAsync();
        res = res.Where(el => (draftChecked && el.Status == CardStatus.Draft) ||
                              (readyChecked && el.Status == CardStatus.Ready) ||
                              (templateChecked && el.Status == CardStatus.Template) ||
                              (archiveChecked && el.Status == CardStatus.Archive)).ToList();
        return res;
    }

    public static async Task<bool> Insert(Card card)
    {
        _ = await BaseDatabase.DB.InsertAsync(card);
        return true;
    }
    public static async Task<bool> Update(Card card)
    {
        _ = await BaseDatabase.DB.UpdateAsync(card);
        return true;
    }

    public static async Task<bool> Remove(int id)
    {
        _ = await BaseDatabase.DB.DeleteAsync<Card>(id);
        return true;
    }

    public static async Task<Card> Get(int id)
    {
        var card = await BaseDatabase.DB.GetAsync<Card>(id);
        return card;
    }

    public static async Task<string> GetLastOrder()
    {
        var card = await BaseDatabase.DB.Table<Card>().Where(el => el.UserId == Settings.UserId).OrderByDescending(el => el.Date).FirstOrDefaultAsync();
        return card == null ? "" : card.Order;
    }
}
