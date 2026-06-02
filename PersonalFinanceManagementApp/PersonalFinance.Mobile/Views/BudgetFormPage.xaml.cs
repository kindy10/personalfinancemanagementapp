using PersonalFinance.Mobile.Helpers;
using PersonalFinance.Mobile.ViewModels;

namespace PersonalFinance.Mobile.Views;

public partial class BudgetFormPage : ContentPage
{
    public BudgetFormPage()
    {
        InitializeComponent();

        var vm =
            new BudgetFormViewModel();

        // Edit mode
        if (TemporaryBudgetData.SelectedBudget != null)
        {
            vm.LoadBudgetAsync(
                TemporaryBudgetData.SelectedBudget);

            TemporaryBudgetData.SelectedBudget = null;
        }

        BindingContext = vm;
    }
}