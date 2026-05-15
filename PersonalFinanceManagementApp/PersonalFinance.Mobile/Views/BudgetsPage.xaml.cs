using PersonalFinance.Mobile.ViewModels;

namespace PersonalFinance.Mobile.Views;

public partial class BudgetsPage : ContentPage
{
	private readonly BudgetsViewModel _viewModel;
	public BudgetsPage()
	{
		InitializeComponent();
		_viewModel = new BudgetsViewModel();

		BindingContext = _viewModel;
	}
	protected override async void OnAppearing()
	{
		base.OnAppearing();
		await _viewModel.LoadBudgets();
	}
}