using OnmpApp.Controls;
using OnmpApp.ViewModels.MainTabs;

namespace OnmpApp.Views.MainTabs;

public partial class CatalogTabPage : MasterContentPage
{
    private CancellationTokenSource textChangedDelayCancellationTokenSource;

    public CatalogTabPage(CatalogTabViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }

    private async void SearchChanged()
    {
        if ((BindingContext as CatalogTabViewModel) == null)
            return;

        textChangedDelayCancellationTokenSource?.Cancel();
        textChangedDelayCancellationTokenSource = new CancellationTokenSource();

        try
        {
            await Task.Delay(1000, textChangedDelayCancellationTokenSource.Token);
            (BindingContext as CatalogTabViewModel).SearchTextChanged();
        }
        catch (TaskCanceledException) { }
    }

    private void EntryNoUnderline_TextChanged(object sender, TextChangedEventArgs e)
    {
        SearchChanged();
    }
}