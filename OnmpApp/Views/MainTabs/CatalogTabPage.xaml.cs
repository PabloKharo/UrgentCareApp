using OnmpApp.Controls;
using OnmpApp.ViewModels.MainTabs;

namespace OnmpApp.Views.MainTabs;

public partial class CatalogTabPage : MasterContentPage
{

    public CatalogTabPage(CatalogTabViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }
}