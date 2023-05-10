using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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

namespace OnmpApp.ViewModels.CardFiller;

[QueryProperty(nameof(CardId), nameof(CardId))]
public partial class EditorPreviewCardViewModel : ObservableObject
{
    
    [ObservableProperty] // Список шаблонов
    ObservableCollection<string> _templates = new();
    [ObservableProperty] // Выбранный шаблон
    string _selectedTemplate;

    [ObservableProperty]
    int _cardId = -1; // Id изменяемой карточки

    [ObservableProperty]
    Card _card; // Карточка

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(NewCard))]
    bool _oldCard = false; // Была ли уже создана карта

    public bool NewCard => !OldCard;

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

    public EditorPreviewCardViewModel() {
        SelectedStatus = CardStatuses[0];
    }

    public async Task<bool> GetTemplates()
    {
        var res = await SearchService.SearchCards("", false, false, true, false);
        Templates = res.Select(el => el.Name).ToObservableCollection();
        Templates.Insert(0, "Без шаблона");
        SelectedTemplate = Templates[0];
        return true;
    }

    [RelayCommand]
    async void ContinueButton()
    {
        
    }

    [RelayCommand]
    async void SaveButton()
    {
        if (OldCard)
        {
            await SearchService.UpdateCard(Card);
        }
        else
        {
            await SearchService.AddCard(Card);
        }
        await Shell.Current.GoToAsync("..", animate: true);
    }

    public async void InitCard()
    {
        if (CardId != -1)
        {
            OldCard = true;
            Card = await SearchService.GetCard(CardId);
            SelectedStatus = Card.Status.ToString();
        }
        else
        {
            OldCard = false;
            Card = new()
            {
                UserId = Settings.UserId,
                Status = CardStatus.Draft,
                Order = await SearchService.CardGetLastOrder()
            };
            SelectedStatus = CardStatuses[0];
            _ = await GetTemplates();

        }
    }
}
