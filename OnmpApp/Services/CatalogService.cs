using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnmpApp.Database;
using OnmpApp.Helpers;
using OnmpApp.Models.Database;

namespace OnmpApp.Services;

public static class CatalogService
{
    public static async Task<List<string>> Search(string search, int skip = 0, int take = 0)
    {
        try
        {
            return await CatalogTable.Search(search, skip, take);
        }
        catch (Exception ex)
        {
#if DEBUG
            Debug.WriteLine(@"\tERROR {0}", ex.Message);
#endif
            ToastHelper.Show(Properties.Resources.Error);
        }
        return null;
    }


    // Удаление карточки
    public static async Task<Catalog> Get(string name)
    {
        try
        {
            return await CatalogTable.Get(name);
        }
        catch (Exception ex)
        {
#if DEBUG
            Debug.WriteLine(@"\tERROR {0}", ex.Message);
#endif
            ToastHelper.Show(Properties.Resources.Error);
        }

        return null;

    }
}