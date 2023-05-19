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
using OnmpApp.Views.CardFiller;
using OnmpApp.Models.Database;

namespace OnmpApp.ViewModels.CardFiller;

[QueryProperty(nameof(CardId), nameof(CardId))]
public partial class EditorPreviewCardViewModel : ObservableObject
{
    
    [ObservableProperty] // Список шаблонов
    ObservableCollection<Card> _templates = new();
    [ObservableProperty] // Выбранный шаблон
    Card _selectedTemplate;

    [ObservableProperty]
    int _cardId = -1; // Id изменяемой карточки

    [ObservableProperty]
    Card _card; // Карточка

    [ObservableProperty]
    TimeSpan _time;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(NewCard))]
    bool _oldCard = false; // Была ли уже создана карта

    public bool NewCard => !OldCard;

    public IList<string> CardStatuses { get; } = Enum.GetValues(typeof(CardStatus))
                                                    .Cast<CardStatus>()
                                                    .Select(e => e.GetDescription())
                                                    .ToList();

    private string _selectedStatus;
    public string SelectedStatus
    {
        get => _selectedStatus;
        set
        {
            _selectedStatus = value;
            if (Card != null)
                Card.Status = StringHelper.GetEnumFromDescription(value);
            OnPropertyChanged(nameof(SelectedStatus));
        }
    }

    public EditorPreviewCardViewModel() {
        SelectedStatus = CardStatuses[0];
    }

    public async Task<bool> GetTemplates()
    {
        var res = await SearchService.SearchCards("", false, false, true, false);
        Templates = res.ToObservableCollection();
        Templates.Insert(0, new Card { Id = -1, Name = "Без шаблона"});
        SelectedTemplate = Templates[0];
        return true;
    }

    async Task Save()
    {
        Card.Date += Time;
        if (OldCard)
        {
            await SearchService.UpdateCard(Card);
        }
        else
        {
            await SearchService.AddCard(Card);
        }
    }

    [RelayCommand]
    async void ContinueButton()
    {
        await Save();
        if (!OldCard && SelectedTemplate.Id != -1)
        {
            
            await Shell.Current.GoToAsync($"{nameof(EditorPreviewCardPage)}/{nameof(TemplateFillerPage)}?CardId={Card.Id}&TemplateId={SelectedTemplate.Id}");
        }
        else 
        {
            await Shell.Current.GoToAsync($"{nameof(EditorPreviewCardPage)}/{nameof(TemplateFillerPage)}?CardId={Card.Id}");

        }
    }

    [RelayCommand]
    async void SaveButton()
    {
        await Save();
        await Shell.Current.GoToAsync("..", animate: true);
    }

    public async void InitCard()
    {
        if (CardId != -1)
        {
            OldCard = true;
            var card = await SearchService.GetCard(CardId);
            Time = card.Date.TimeOfDay;
            Card = card;
            SelectedStatus = Card.Status.GetDescription();
        }
        else
        {
            OldCard = false;
            var card = new Card()
            {
                UserId = Settings.UserId,
                Status = CardStatus.Draft,
                Order = await SearchService.CardGetLastOrder(),
            };

            Time = card.Date.TimeOfDay;
            Card = card;
            SelectedStatus = CardStatuses[0];
            _ = await GetTemplates();

        }

    }
}
