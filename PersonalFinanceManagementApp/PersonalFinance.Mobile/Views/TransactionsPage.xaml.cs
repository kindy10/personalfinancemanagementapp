using PersonalFinance.Mobile.ViewModels;

namespace PersonalFinance.Mobile.Views;

public partial class TransactionsPage : ContentPage
{
	private readonly TransactionsViewModel _viewModel;
	public TransactionsPage()
	{
		InitializeComponent();
		_viewModel = new TransactionsViewModel();
		BindingContext = _viewModel;
	}
	protected override async void OnAppearing()
	{
		base.OnAppearing();

		//Reload transactions everytime page appears
		await _viewModel.LoadTransactions();
	}
}