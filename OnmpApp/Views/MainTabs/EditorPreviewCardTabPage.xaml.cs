using OnmpApp.Controls;
using OnmpApp.ViewModels.MainTabs;

namespace OnmpApp.Views.MainTabs;

public partial class EditorPreviewCardTabPage : MasterContentPage
{
	public EditorPreviewCardTabPage(EditorPreviewCardTabViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}

    private void MasterContentPage_Appearing(object sender, EventArgs e)
    {
		(BindingContext as EditorPreviewCardTabViewModel).InitCard();
    }
}