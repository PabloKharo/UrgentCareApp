using CommunityToolkit.Maui.Core.Extensions;
using OnmpApp.Helpers;
using OnmpApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnmpApp.Services.Database;


namespace OnmpApp.Services.MainTabs;

public static class SearchService
{
    // Получение списка карточек
    public async static Task<List<Card>> SearchCards(string searchText, bool draftChecked, bool readyChecked, 
                                        bool templateChecked, bool archiveChecked, int skip = 0, int take = 20)
    {
        try
        {
            var res = await DatabaseService.CardsSearch(searchText, draftChecked, readyChecked, templateChecked, archiveChecked, skip, take);
            return res;
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

    // Обновление карточки
    public async static Task<bool> UpdateCard(Card card)
    {
        try
        {
            _ = await DatabaseService.CardUpdate(card);
            return true;
        }
        catch (Exception ex)
        {
#if DEBUG
            Debug.WriteLine(@"\tERROR {0}", ex.Message);
#endif
            ToastHelper.Show(Properties.Resources.Error);
        }
        return false;

    }

    public static async Task<string> CardGetLastOrder()
    {
        return await DatabaseService.CardGetLastOrder();
    }


    // Удаление карточки
    public async static Task<bool> RemoveCard(Card card)
    {
        try
        {
            _ = await DatabaseService.CardRemove(card);
            return true;
        }
        catch (Exception ex)
        {
#if DEBUG
            Debug.WriteLine(@"\tERROR {0}", ex.Message);
#endif
            ToastHelper.Show(Properties.Resources.Error);
        }

        return false;

    }


    // Получение краткой информации карточки
    public async static Task<Card> GetCard(int id)
    {
        try
        {
            var res = await DatabaseService.CardGet(id);
            return res;
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

    // Добавление карточки
    public async static Task<bool> AddCard(Card card)
    {
        try
        {
            _ = await DatabaseService.CardCreate(card);
            return true;
        }
        catch (Exception ex)
        {
#if DEBUG
            Debug.WriteLine(@"\tERROR {0}", ex.Message);
#endif
            ToastHelper.Show(Properties.Resources.Error);
        }

        return false;
    }


}
