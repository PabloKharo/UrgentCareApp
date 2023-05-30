using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OnmpApp.Database;
using OnmpApp.Models.Database;
using OnmpApp.Services;
using OnmpApp.ViewModels.Catalog;
using OnmpApp.Views.Catalog;

namespace OnmpApp.ViewModels.MainTabs;
public partial class CatalogTabViewModel : ObservableObject
{
    [ObservableProperty]
    string _searchText = "";

    [ObservableProperty]
    ObservableCollection<CatalogShort> _catalogElements = new();


    [ObservableProperty]
    bool _isRefreshing = false;

    public CatalogTabViewModel()
    {

    }

    public async void SearchItems()
    {
        if (string.IsNullOrWhiteSpace(SearchText))
        {
            IsRefreshing = false;
            return;
        }

        IsRefreshing = true;

        var res = await CatalogService.Search(SearchText);
        CatalogElements = res.OrderBy(el => el.Name).ToObservableCollection();

        IsRefreshing = false;
    }

    [RelayCommand] // Нажатие на карту
    async void ItemTapped(CatalogShort selectedCatalog)
    {
        if (selectedCatalog == null)
            return;

        await Shell.Current.GoToAsync($"{nameof(CatalogTextPage)}?Name={selectedCatalog.Name}");
    }

    [RelayCommand] // Добавление элементов, которые не были показаны
    static async void RemainingItemsThresholdReached()
    {

    }

}
