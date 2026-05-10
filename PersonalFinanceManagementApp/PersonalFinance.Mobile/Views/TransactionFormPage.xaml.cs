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
		if(TemporaryData.selectedTranaction != null)
		{
			vm.LoadTransaction(TemporaryData.selectedTranaction);

			//Clear tmeporary state
			TemporaryData.selectedTranaction = null;
		}
		BindingContext = vm;
	}
}