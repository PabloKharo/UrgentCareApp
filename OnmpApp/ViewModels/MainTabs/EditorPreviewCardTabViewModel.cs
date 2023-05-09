using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OnmpApp.Data;
using OnmpApp.Models;
using OnmpApp.Properties;
using OnmpApp.Services.MainTabs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using OnmpApp.Helpers;

namespace OnmpApp.ViewModels.MainTabs;

[QueryProperty(nameof(CardId), nameof(CardId))]
public partial class EditorPreviewCardTabViewModel : ObservableObject
{
    
    [ObservableProperty] // Список шаблонов
    ObservableCollection<string> _templates = new();
    [ObservableProperty] // Выбранный шаблон
    int _selectedIndex = 0;

    [ObservableProperty]
    int _cardId = -1; // Id изменяемой карточки

    [ObservableProperty]
    Card _card; // Карточка

    [ObservableProperty]
    bool _oldCard = false; // Была ли уже создана карта

    public IList<string> CardStatuses { get; } = Enum.GetNames(typeof(CardStatus)).ToList();

    private string _selectedStatus;
    public string SelectedStatus
    {
        get => _selectedStatus;
        set
        {
            _selectedStatus = value;
            if (Card != null)
                Card._status = value;
            OnPropertyChanged(nameof(SelectedStatus));
        }
    }

    public EditorPreviewCardTabViewModel() {
        GetTemplates();
        SelectedStatus = CardStatuses[0];
    }

    public async void GetTemplates()
    {
        var res = await SearchService.SearchCards("", false, false, true, false);
        Templates = res.Select(el => el.Name).ToObservableCollection();
    }

    [RelayCommand]
    async void ContinueButton()
    {
        
    }

    [RelayCommand]
    async void SaveButton()
    {
        await SearchService.AddCard(Card);
        await Shell.Current.GoToAsync("..", animate: true);
    }

    public async void InitCard()
    {
        if (CardId != -1)
        {
            Card = await SearchService.GetCard(CardId);
            SelectedStatus = Card.Status.ToString();
            OldCard = true;
        }
        else
        {
            Card = new()
            {
                UserId = Settings.UserId,
                Status = CardStatus.Draft,
                Order = await Database.CardGetLastOrder()
            };
            SelectedStatus = CardStatuses[0];
        }
    }
}
