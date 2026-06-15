using PersonalFinance.Mobile.Helpers;
using PersonalFinance.Mobile.ViewModels;

namespace PersonalFinance.Mobile.Views;
public partial class BudgetFormPage : ContentPage
{
    private readonly BudgetFormViewModel _vm;

    public BudgetFormPage()
    {
        InitializeComponent();

        _vm = new BudgetFormViewModel();
        BindingContext = _vm;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (TemporaryBudgetData.SelectedBudget != null)
        {
            await _vm.LoadBudgetAsync(
                TemporaryBudgetData.SelectedBudget);

            TemporaryBudgetData.SelectedBudget = null;
        }
        else
        {
            await _vm.LoadCategoriesAsync();
        }
    }
}
