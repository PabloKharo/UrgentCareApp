﻿using CommunityToolkit.Mvvm.ComponentModel;
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
using OnmpApp.Services;
using OnmpApp.ViewModels.CardFiller;
using System.Windows.Input;
using CommunityToolkit.Maui.Core.Extensions;
using OnmpApp.Models.Database;
using OnmpApp.Views.MainTabs;
using OnmpApp.Views.CardFiller;

namespace OnmpApp.ViewModels.MainTabs;

public partial class SearchTabViewModel : ObservableObject
{
    // TODO: Добавить фильтр для дат

    
    [ObservableProperty]
    string _searchText = "";

    [ObservableProperty]
    ObservableCollection<Card> _smallCards = new();

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
        await SearchTextChanged();
        IsRefreshing = false;
    }

    [RelayCommand] // Нажатие на карту
    async void ItemTapped(Card selectedCard)
    {
        if (selectedCard == null)
            return;

        await Shell.Current.GoToAsync($"{nameof(EditorPreviewCardPage)}?CardId={selectedCard.Id}");
    }

    [RelayCommand] // Переключение видимости фильтров
    void ToggleFilters()
    {
        FiltersShown = !FiltersShown;
    }

    [RelayCommand] // Добавление элементов, которые не были показаны
    static async void RemainingItemsThresholdReached()
    {
        
    }

    [RelayCommand] // Добавление элементов, которые не были показаны
    async void CreateCard()
    {
        await Shell.Current.GoToAsync($"{nameof(EditorPreviewCardPage)}");
    }

    // Поиск элемента при изменении запроса
    public async Task SearchTextChanged()
    {
        var res = await CardService.Search(SearchText, DraftChecked, ReadyChecked, TemplateChecked, ArchiveChecked);
        SmallCards = res.OrderByDescending(el => el.Id).ToObservableCollection();
    }

    // Удаление элемента
    public async void ItemDelete(Card selectedCard)
    {
        if (selectedCard == null)
            return;

        await CardService.Remove(selectedCard.Id);
        SmallCards.Remove(selectedCard);

    }

    // Архивация элемента
    public async void ItemArchive(Card selectedCard)
    {
        if (selectedCard == null)
            return;

        if (selectedCard.Status != CardStatus.Archive)
            selectedCard.Status = CardStatus.Archive;
        else
            selectedCard.Status = CardStatus.Ready;

        await CardService.Update(selectedCard);

        if(selectedCard.Status == CardStatus.Archive && !ArchiveChecked)
        {
            SmallCards.Remove(selectedCard);
            return;
        }

        SmallCards[SmallCards.IndexOf(selectedCard)] = selectedCard;
    }

}
