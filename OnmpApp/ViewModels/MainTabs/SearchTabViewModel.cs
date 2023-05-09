using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OnmpApp.Views.Authorize;
using OnmpApp.Views.UserSettings;
using OnmpApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using OnmpApp.Services.MainTabs;
using System.Windows.Input;
using CommunityToolkit.Maui.Core.Extensions;
using OnmpApp.Views.MainTabs;

namespace OnmpApp.ViewModels.MainTabs;

public partial class SearchTabViewModel : ObservableObject
{
    // TODO: Добавить сортировку
    // TODO: Добавить фильтр для дат

    
    [ObservableProperty]
    string _searchText = "";

    [ObservableProperty]
    ObservableCollection<Card> _smallCards = new ObservableCollection<Card>();

    [ObservableProperty]
    bool _filtersShown = false;

    // Поля, определяющие, какие типы карт искать
    [ObservableProperty]
    bool _draftChecked = true;
    [RelayCommand]
    void CheckDraft() => DraftChecked = !DraftChecked;

    [ObservableProperty]
    bool _readyChecked = true;
    [RelayCommand]
    void CheckReady() => ReadyChecked = !ReadyChecked;

    [ObservableProperty]
    bool _templateChecked = false;
    [RelayCommand]
    void CheckTemplate() => TemplateChecked = !TemplateChecked;

    [ObservableProperty]
    bool _archiveChecked = false;
    [RelayCommand]
    void CheckArchive() => ArchiveChecked = !ArchiveChecked;


    [ObservableProperty]
    bool _isRefreshing = false;

    public SearchTabViewModel() {
        SearchTextChanged();
    }

    [RelayCommand]
    async Task NavigateToSettingsPage()
    {
        await Shell.Current.GoToAsync(nameof(SettingsPage));
    }

    [RelayCommand]
    async void Refresh()
    {
        IsRefreshing = true;
        SearchTextChanged();
        IsRefreshing = false;
    }

    [RelayCommand] // Нажатие на карту
    async void ItemTapped(Card selectedCard)
    {
        if (selectedCard == null)
            return;

        await Shell.Current.GoToAsync($"{nameof(EditorPreviewCardTabPage)}?CardId={selectedCard.Id}");
    }

    [RelayCommand] // Переключение видимости фильтров
    async void ToggleFilters()
    {
        FiltersShown = !FiltersShown;
    }

    [RelayCommand] // Добавление элементов, которые не были показаны
    async void RemainingItemsThresholdReached()
    {
        
    }

    [RelayCommand] // Добавление элементов, которые не были показаны
    async void CreateCard()
    {
        await Shell.Current.GoToAsync($"{nameof(EditorPreviewCardTabPage)}");
    }

    // Поиск элемента при изменении запроса
    public async void SearchTextChanged()
    {
        if (SearchText == null)
            return;

        var res = await SearchService.SearchCards(SearchText, DraftChecked, ReadyChecked, TemplateChecked, ArchiveChecked);
        SmallCards = res.OrderByDescending(el => el.Date).ToObservableCollection();
    }

    // Удаление элемента
    public async void ItemDelete(Card selectedCard)
    {
        if (selectedCard == null)
            return;

        SearchService.RemoveCard(selectedCard);
        SmallCards.Remove(selectedCard);

    }

    // Архивация элемента
    public void ItemArchive(Card selectedCard)
    {
        if (selectedCard == null)
            return;

        SmallCards.Where(el => el == selectedCard).ToList().FirstOrDefault().Status = 
                ((selectedCard.Status == CardStatus.Ready) ? CardStatus.Archive : CardStatus.Ready);
    }

}
