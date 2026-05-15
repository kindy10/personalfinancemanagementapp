using PersonalFinance.Mobile.Helpers;
using PersonalFinance.Mobile.ViewModels;

namespace PersonalFinance.Mobile.Views;

public partial class CategoryFormPage : ContentPage
{
	public CategoryFormPage()
	{
		InitializeComponent();
		
		var vm = new CategoryFormViewModel();

		//Check edit mode
		if(TemporaryCategoryData.SelectedCategory != null)
		{
			vm.LoadCategory(TemporaryCategoryData.SelectedCategory);

			TemporaryCategoryData.SelectedCategory = null;
		}
		BindingContext = vm;
	}
}