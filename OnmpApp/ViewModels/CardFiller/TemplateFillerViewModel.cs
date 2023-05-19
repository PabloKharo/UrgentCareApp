using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OnmpApp.Models;
using OnmpApp.Models.Database;
using OnmpApp.Services.Database;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnmpApp.ViewModels.CardFiller;

[QueryProperty(nameof(CardId), nameof(CardId))]
[QueryProperty(nameof(TemplateId), nameof(TemplateId))]
public partial class TemplateFillerViewModel : ObservableObject
{
    [ObservableProperty]
    ObservableCollection<TestQuestion> _questions = new ObservableCollection<TestQuestion>();

    [ObservableProperty]
    bool _isFinishButtonVisible = false;

    [ObservableProperty]
    int _cardId = -1;

    [ObservableProperty]
    int _templateId = -1;

    [ObservableProperty]
    FullCard card = null;


    public TemplateFillerViewModel()
    {
        
    }

    public async void InitCard()
    {
        Card = await DatabaseService.FullCardGet(CardId);

        if(TemplateId > 0)
        {
            var card = await DatabaseService.FullCardGet(TemplateId);
            card.Id = CardId;
            Card = card;
        }

        var tmp = TestQuestionsFactory.CreateFromAttributes(Card);
        Questions = new ObservableCollection<TestQuestion>(tmp);
        await DatabaseService.FullCardUpdate(Card);
    }

    public void SaveTestResults(FullCard fullCard)
    {
        if (fullCard == null) throw new ArgumentNullException(nameof(fullCard));

        var properties = typeof(FullCard).GetProperties();

        for (int i = 0; i < Questions.Count; i++)
        {
            var property = properties[i+1];
            var question = Questions[i];

            if (property.PropertyType == typeof(int) || property.PropertyType == typeof(double))
            {
                if(property.PropertyType == typeof(int) && int.TryParse(question.GetValue(), out int res1))
                    property.SetValue(fullCard, res1);
                else if (property.PropertyType == typeof(double) && double.TryParse(question.GetValue().Replace('.', ','), out double res2))
                    property.SetValue(fullCard, res2);
                else
                    property.SetValue(fullCard, 0);
            }
            else
            {
                property.SetValue(fullCard, question.GetValue());
            }

        }
    }

    [RelayCommand]
    async void ButtonPressed()
    {
        SaveTestResults(Card);
        await DatabaseService.FullCardUpdate(Card);
        await Shell.Current.GoToAsync("../..", animate: true);
    }

}
