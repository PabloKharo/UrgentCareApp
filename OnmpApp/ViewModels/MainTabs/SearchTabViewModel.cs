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
using System.Windows.Input;
using CommunityToolkit.Maui.Core.Extensions;

namespace OnmpApp.ViewModels.MainTabs;

public partial class SearchTabViewModel : ObservableObject
{
    // TODO: Добавить сортировку
    // TODO: Добавить фильтр для дат

    [ObservableProperty]
    string _searchText = "";

    [ObservableProperty]
    ObservableCollection<SmallCard> _smallCards = new ObservableCollection<SmallCard>();

    [ObservableProperty]
    bool _filtersShown = false;


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


    List<SmallCard> initCards = new List<SmallCard>
    {
        new SmallCard { Name="ОРВИ", Date = DateTime.Now, Type=CardType.Ready},
        new SmallCard { Name="Грипп", Date = DateTime.Now, Type=CardType.Archive},
        new SmallCard { Name="Ангина", Date = DateTime.Now, Type=CardType.Template},
        new SmallCard { Name="Холера", Date = DateTime.Now, Type=CardType.Draft},
        new SmallCard { Name="Инфекция", Date = DateTime.Now, Type=CardType.Ready},
        new SmallCard { Name="Аллергия", Date = DateTime.Now, Type=CardType.Ready},
        new SmallCard { Name="Астма", Date = DateTime.Now, Type=CardType.Ready},
        new SmallCard { Name="Диабет", Date = DateTime.Now, Type=CardType.Ready},
        new SmallCard { Name="Акне", Date = DateTime.Now, Type=CardType.Ready},
        new SmallCard { Name="Гастрит", Date = DateTime.Now, Type=CardType.Ready},
        new SmallCard { Name="ОРВИ", Date = DateTime.Now, Type=CardType.Ready},
        new SmallCard { Name="Грипп", Date = DateTime.Now, Type=CardType.Archive},
        new SmallCard { Name="Ангина", Date = DateTime.Now, Type=CardType.Template},
        new SmallCard { Name="Холера", Date = DateTime.Now, Type=CardType.Draft},
        new SmallCard { Name="Инфекция", Date = DateTime.Now, Type=CardType.Ready},
        new SmallCard { Name="Аллергия", Date = DateTime.Now, Type=CardType.Ready},
        new SmallCard { Name="Астма", Date = DateTime.Now, Type=CardType.Ready},
        new SmallCard { Name="Диабет", Date = DateTime.Now, Type=CardType.Ready},
        new SmallCard { Name="Акне", Date = DateTime.Now, Type=CardType.Ready},
        new SmallCard { Name="Гастрит", Date = DateTime.Now, Type=CardType.Ready},
    };

    public SearchTabViewModel() {
        SearchTextChanged();
    }

    [RelayCommand]
    async Task NavigateToSettingsPage()
    {
        await Shell.Current.GoToAsync(nameof(SettingsPage));
    }

    [RelayCommand]
    async void ItemTapped(SmallCard selectedCard)
    {
        if (selectedCard == null)
            return;

        await Microsoft.Maui.Controls.Application.Current.MainPage.DisplayAlert("Уведомление", "Вы нажали на поле", "OK");
    }

    [RelayCommand]
    async void ToggleFilters()
    {
        FiltersShown = !FiltersShown;
    }

    [RelayCommand]
    async void RemainingItemsThresholdReached()
    {
        
    }

    public void SearchTextChanged()
    {
        if (SearchText == null)
            return;

        SmallCards = initCards.Where(el => el.Name.Contains(SearchText) &&
                    ((DraftChecked ? el.Type == CardType.Draft : false) ||
                    (ReadyChecked ? el.Type == CardType.Ready : false) ||
                    (TemplateChecked ? el.Type == CardType.Template : false) ||
                    (ArchiveChecked ? el.Type == CardType.Archive : false))
                ).ToObservableCollection();
    }

    public void ItemDelete(SmallCard selectedCard)
    {
        if (selectedCard == null)
            return;

        initCards.Remove(selectedCard);
        SmallCards.Remove(selectedCard);

    }

    public void ItemArchive(SmallCard selectedCard)
    {
        if (selectedCard == null)
            return;

        SmallCards.Where(el => el == selectedCard).ToList().FirstOrDefault().Type = 
                ((selectedCard.Type == CardType.Ready) ? CardType.Archive : CardType.Ready);
    }

}
