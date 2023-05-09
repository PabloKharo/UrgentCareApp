using System.Threading;
using OnmpApp.Controls;
using OnmpApp.Models;
using OnmpApp.ViewModels.MainTabs;

namespace OnmpApp.Views.MainTabs;

public partial class SearchTabPage : MasterContentPage
{
	private CancellationTokenSource textChangedDelayCancellationTokenSource;
	SearchTabViewModel vm;

	public SearchTabPage(SearchTabViewModel _vm)
	{
		InitializeComponent();
		BindingContext = _vm;
		vm = _vm;
	}

	private void OnDeleteSwipeItemInvoked(object sender, EventArgs e)
	{
		if (vm == null)
			return;

		var swipeItemView = (SwipeItem)sender;
		var smallCard = (Card)swipeItemView.BindingContext;

		vm.ItemDelete(smallCard);
	}

	private void OnArchiveSwipeItemInvoked(object sender, EventArgs e)
	{
		if (vm == null)
			return;

		var swipeItemView = (SwipeItem)sender;
		var smallCard = (Card)swipeItemView.BindingContext;

		vm.ItemArchive(smallCard);
	}

	private async void SearchChanged()
	{
		if (vm == null)
			return;

		textChangedDelayCancellationTokenSource?.Cancel();
		textChangedDelayCancellationTokenSource = new CancellationTokenSource();

		try
		{
			await Task.Delay(1000, textChangedDelayCancellationTokenSource.Token);
			vm.SearchTextChanged();
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
}