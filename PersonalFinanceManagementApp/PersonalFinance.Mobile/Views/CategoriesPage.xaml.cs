using PersonalFinance.Mobile.ViewModels;

namespace PersonalFinance.Mobile.Views;

public partial class CategoriesPage : ContentPage
{
	private readonly CategoriesViewModel _viewModel;
	public CategoriesPage()
	{
		InitializeComponent();
		_viewModel = new CategoriesViewModel();
		BindingContext = _viewModel;
	}

	protected override async void OnAppearing()
	{
		base.OnAppearing();

		await _viewModel.LoadCategories();
	}
}