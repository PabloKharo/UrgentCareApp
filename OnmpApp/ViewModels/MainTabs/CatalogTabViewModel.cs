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
using OnmpApp.Services;
using OnmpApp.ViewModels.Catalog;
using OnmpApp.Views.Catalog;

namespace OnmpApp.ViewModels.MainTabs;
public partial class CatalogTabViewModel : ObservableObject
{
    [ObservableProperty]
    string _searchText = "";

    [ObservableProperty] private ObservableCollection<string> _catalogElements;


    [ObservableProperty]
    bool _isRefreshing = false;

    public CatalogTabViewModel()
    {
        _ = SearchTextChanged();
    }

    [RelayCommand]
    async void Refresh()
    {
        IsRefreshing = true;
        await SearchTextChanged();
        IsRefreshing = false;
    }

    [RelayCommand] // Нажатие на карту
    async void ItemTapped(string selectedCatalog)
    {
        if (selectedCatalog == null)
            return;

        await Shell.Current.GoToAsync($"{nameof(CatalogTextPage)}?Name={selectedCatalog}");
    }

    [RelayCommand] // Добавление элементов, которые не были показаны
    static async void RemainingItemsThresholdReached()
    {

    }

    // Поиск элемента при изменении запроса
    public async Task SearchTextChanged()
    {
        var res = await CatalogService.Search(SearchText);
        CatalogElements = res.ToObservableCollection();
    }

}
