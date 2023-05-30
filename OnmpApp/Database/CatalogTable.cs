using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnmpApp.Models.Database;

namespace OnmpApp.Database;

public static class CatalogTable
{
    public static async Task<List<CatalogShort>> Search(string search, int skip = 0, int take = 0)
    {
        string query = "SELECT Name, ElType, CASE WHEN Text IS NULL THEN 0 ELSE 1 END AS Loaded FROM Catalogs WHERE Name LIKE ?";
        var res = await BaseDatabase.DB.QueryAsync<CatalogShort>(query, $"%{search}%");
        return res.ToList();
    }

    public static async Task<Catalog> Get(string name)
    {
        var card = await BaseDatabase.DB.Table<Catalog>().Where(el => el.Name == name).FirstOrDefaultAsync();
        return card;
    }

    public static async Task<bool> Update(Catalog catalog)
    {
        _ = await BaseDatabase.DB.UpdateAsync(catalog);
        return true;
    }

    public static async Task<bool> Insert(string name, CatalogType type, string text=null)
    {
        _ = await BaseDatabase.DB.InsertAsync(new Catalog
        {
            Name = name,
            ElType = type,
            Text = text
        });
        return true;
    }
}
