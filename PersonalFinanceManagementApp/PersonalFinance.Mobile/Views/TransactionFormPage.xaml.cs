using PersonalFinance.Mobile.Helpers;
using PersonalFinance.Mobile.ViewModels;

namespace PersonalFinance.Mobile.Views;
public partial class TransactionFormPage : ContentPage
{
    private readonly TransactionFormViewModel _vm;

    public TransactionFormPage()
    {
        InitializeComponent();

        _vm = new TransactionFormViewModel();
        BindingContext = _vm;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        //check if editing existing transaction
        if (TemporaryData.selectedTransaction != null)
        {
            await _vm.LoadTransactionAsync(
                TemporaryData.selectedTransaction);

            //Clear tmeporary state
            TemporaryData.selectedTransaction = null;
        }
        else
        {
            await _vm.LoadCategoriesAsync();
        }
    }
}