using PersonalFinance.Mobile.Helpers;
using PersonalFinance.Mobile.ViewModels;

namespace PersonalFinance.Mobile.Views;

public partial class TransactionFormPage : ContentPage
{
	public TransactionFormPage()
	{
		InitializeComponent();
		var vm = new TransactionFormViewModel();

		//check if editing existing transaction
		if(TemporaryData.selectedTransaction != null)
		{
			vm.LoadTransactionAsync(TemporaryData.selectedTransaction);

			//Clear tmeporary state
			TemporaryData.selectedTransaction = null;
		}
		BindingContext = vm;
	}
}