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

namespace OnmpApp.Services.MainTabs;

public static class SearchService
{
    static List<PreviewCard> initCards = new List<PreviewCard>
    {
        new PreviewCard { Id = 0, Name="ОРВИ", Date = DateTime.Now, Type=CardType.Ready},
        new PreviewCard { Id = 1, Name="Грипп", Date = DateTime.Now, Type=CardType.Archive},
        new PreviewCard { Id = 2, Name="Ангина", Date = DateTime.Now, Type=CardType.Template},
        new PreviewCard { Id = 3, Name="Холера", Date = DateTime.Now, Type=CardType.Draft},
        new PreviewCard { Id = 4, Name="Инфекция", Date = DateTime.Now, Type=CardType.Ready},
        new PreviewCard { Id = 5, Name="Аллергия", Date = DateTime.Now, Type=CardType.Ready},
        new PreviewCard { Id = 6, Name="Астма", Date = DateTime.Now, Type=CardType.Ready},
        new PreviewCard { Id = 7, Name="Диабет", Date = DateTime.Now, Type=CardType.Ready},
        new PreviewCard { Id = 8, Name="Акне", Date = DateTime.Now, Type=CardType.Ready},
        new PreviewCard { Id = 9, Name="Гастрит", Date = DateTime.Now, Type=CardType.Ready},
    };

    // Получение списка карточек
    public static ObservableCollection<PreviewCard> SearchPreviewCards(string searchText, bool draftChecked, bool readyChecked, 
                                        bool templateChecked, bool archiveChecked)
    {
        try
        {
            return initCards.Where(el => el.Name.Contains(searchText) &&
                    ((draftChecked ? el.Type == CardType.Draft : false) ||
                    (readyChecked ? el.Type == CardType.Ready : false) ||
                    (templateChecked ? el.Type == CardType.Template : false) ||
                    (archiveChecked ? el.Type == CardType.Archive : false))
                ).ToObservableCollection();
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
    public static void RemovePreviewCard(PreviewCard card)
    {
        try
        {
            initCards.Remove(card);
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
    public static PreviewCard GetPreviewCard(int id)
    {
        try
        {
            return initCards.Where(el => el.Id == id).FirstOrDefault();
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

    // Получение id для добавления в БД
    public static int CreateNewId()
    {
        try
        {
            return initCards.Last().Id + 1;
        }
        catch (Exception ex)
        {
#if DEBUG
            Debug.WriteLine(@"\tERROR {0}", ex.Message);
#endif
            ToastHelper.Show(Properties.Resources.Error);
        }

        return 0;
    }

    // Добавление карточки
    public static bool AddCard(PreviewCard card)
    {
        try
        {
            initCards.Add(card);
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
