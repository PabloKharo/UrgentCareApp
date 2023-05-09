using CommunityToolkit.Maui.Core.Extensions;
using OnmpApp.Data;
using OnmpApp.Helpers;
using OnmpApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnmpApp.Services.MainTabs;

public static class SearchService
{
    // Получение списка карточек
    public async static Task<List<Card>> SearchCards(string searchText, bool draftChecked, bool readyChecked, 
                                        bool templateChecked, bool archiveChecked, int skip = 0, int take = 20)
    {
        try
        {
            var res = await Database.CardsSearch(searchText, draftChecked, readyChecked, templateChecked, archiveChecked, skip, take);
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

   
    // Удаление карточки
    public async static void RemoveCard(Card card)
    {
        try
        {
            _ = await Database.CardRemove(card);
        }
        catch (Exception ex)
        {
#if DEBUG
            Debug.WriteLine(@"\tERROR {0}", ex.Message);
#endif
            ToastHelper.Show(Properties.Resources.Error);
        }

    }


    // Получение краткой информации карточки
    public async static Task<Card> GetCard(int id)
    {
        try
        {
            var res = await Database.CardGet(id);
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
            _ = await Database.CardCreate(card);
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
