using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OnmpApp.Models;
using OnmpApp.Services.MainTabs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    PreviewCard _card; // Карточка

    [ObservableProperty]
    bool _oldCard = false; // Была ли уже создана карта

    public EditorPreviewCardTabViewModel() {
        Templates = new ObservableCollection<string>
        {
            "Без шаблона",
            "Дети",
            "Взрослые",
            "ОРВИ",
            "Дети",
            "Взрослые",
            "ОРВИ",
            
        };
    }

    [RelayCommand]
    async void ContinueButton()
    {
        
    }

    [RelayCommand]
    async void SaveButton()
    {
        SearchService.AddCard(Card);
        await Shell.Current.GoToAsync("..", animate: true);
    }

    public void InitCard()
    {
        if (CardId != -1)
        {
            Card = SearchService.GetPreviewCard(CardId);

            OldCard = true;
        }
        else
        {
            Card = new()
            {
                Type = CardType.Draft,
                Id = SearchService.CreateNewId(),
                Date = DateTime.Now
            };
        }
    }
}
