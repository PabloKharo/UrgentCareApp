using System.Threading;
using OnmpApp.Controls;
using OnmpApp.Models;
using OnmpApp.Models.Database;
using OnmpApp.ViewModels.MainTabs;

namespace OnmpApp.Views.MainTabs;

public partial class SearchTabPage : MasterContentPage
{
	private CancellationTokenSource textChangedDelayCancellationTokenSource;

	public SearchTabPage(SearchTabViewModel _vm)
	{
		InitializeComponent();
		BindingContext = _vm;
	}

	private void OnDeleteSwipeItemInvoked(object sender, EventArgs e)
	{
		if ((BindingContext as SearchTabViewModel) == null)
			return;

		var swipeItemView = (SwipeItem)sender;
		var smallCard = (Card)swipeItemView.BindingContext;

        (BindingContext as SearchTabViewModel).ItemDelete(smallCard);
	}

	private void OnArchiveSwipeItemInvoked(object sender, EventArgs e)
	{
		if ((BindingContext as SearchTabViewModel) == null)
			return;

		var swipeItemView = (SwipeItem)sender;
		var smallCard = (Card)swipeItemView.BindingContext;

        (BindingContext as SearchTabViewModel).ItemArchive(smallCard);
	}

	private async void SearchChanged()
	{
		if ((BindingContext as SearchTabViewModel) == null)
			return;

		textChangedDelayCancellationTokenSource?.Cancel();
		textChangedDelayCancellationTokenSource = new CancellationTokenSource();

		try
		{
			await Task.Delay(1000, textChangedDelayCancellationTokenSource.Token);
			await (BindingContext as SearchTabViewModel).SearchTextChanged();
		}
		catch (TaskCanceledException) { }
	}

	private void EntryNoUnderline_TextChanged(object sender, TextChangedEventArgs e)
	{
		SearchChanged();
	}

	private void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
	{
		SearchChanged();
	}

    private async void MasterContentPage_Appearing(object sender, EventArgs e)
    {
        if ((BindingContext as SearchTabViewModel) == null)
            return;

        await (BindingContext as SearchTabViewModel).SearchTextChanged();
    }
}