using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OnmpApp.Models;
using OnmpApp.Models.Database;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnmpApp.ViewModels.CardFiller;

[QueryProperty(nameof(CardId), nameof(CardId))]
public partial class TemplateFillerViewModel : ObservableObject
{
    [ObservableProperty]
    ObservableCollection<TestQuestion> _questions = new ObservableCollection<TestQuestion>();

    [ObservableProperty]
    bool _isFinishButtonVisible = false;

    [ObservableProperty]
    int _cardId = -1;

    [ObservableProperty]
    FullCard card = null;


    public TemplateFillerViewModel()
    {
        var tmp = TestQuestionsFactory.CreateFromAttributes<FullCard>(card);
        Questions = new ObservableCollection<TestQuestion>(tmp);
    }

    public void SaveTestResults(FullCard fullCard)
    {
        if (fullCard == null) throw new ArgumentNullException(nameof(fullCard));

        var properties = typeof(FullCard).GetProperties();

        for (int i = 0; i < Questions.Count; i++)
        {
            var property = properties[i];
            var question = Questions[i];

            property.SetValue(fullCard, question.GetValue());

        }
    }

    [RelayCommand]
    void ButtonPressed()
    {
        var fullCard = new FullCard();
        SaveTestResults(fullCard);
        /*var tmp = TestQuestionsFactory.CreateFromAttributes<FullCard>(fullCard);
        Questions = new ObservableCollection<TestQuestion>(tmp);*/

    }

}
