using OnmpApp.Controls;
using OnmpApp.Models;
using OnmpApp.Models.Database;
using OnmpApp.ViewModels.CardFiller;

namespace OnmpApp.Views.CardFiller;

public partial class TemplateFillerPage : MasterContentPage
{
	TemplateFillerViewModel vm;
	public TemplateFillerPage(TemplateFillerViewModel _vm)
	{
		InitializeComponent();
        BindingContext = _vm;
        vm = _vm;
    }

    private void QuestionsCarouselView_CurrentItemChanged(object sender, CurrentItemChangedEventArgs e)
    {
        var carouselView = sender as CarouselView;
        var currentItemIndex = carouselView.ItemsSource.Cast<TestQuestion>().ToList().IndexOf(e.CurrentItem as TestQuestion);

        vm.IsFinishButtonVisible = currentItemIndex == vm.Questions.Count - 1;
    }
}