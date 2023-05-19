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

    private void MasterContentPage_Appearing(object sender, EventArgs e)
    {
        (BindingContext as TemplateFillerViewModel).InitCard();
    }
}