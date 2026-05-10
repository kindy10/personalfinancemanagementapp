using Microsoft.IdentityModel.Tokens;
using PersonalFinance.Mobile.Helpers;
using PersonalFinance.Mobile.Services;
using PersonalFinance.Shared.DTOs.Categories;
using PersonalFinance.Shared.DTOs.Transactions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PersonalFinance.Mobile.ViewModels
{
    public  class TransactionsViewModel :BaseViewModel
    {
        private readonly TransactionService _transactionService;

   

        //Collection automatically updates UI
        public ObservableCollection<TransactionDto> Transactions { get; set; }


        //Command for deleting transaction
        public ICommand DeleteCommand { get; }

        //Command for Navigation
        public ICommand GoToAddCommand { get; }

        //Command for editing
        public ICommand EditCommand { get; }
             
        public TransactionsViewModel()
        {
            _transactionService = new TransactionService();

            Transactions = new ObservableCollection<TransactionDto>();

            EditCommand =new Command<TransactionDto>( async (transaction) =>await EditTransaction(transaction));

            DeleteCommand = new Command<Guid>(async (id) => await DeleteTransaction(id));



            GoToAddCommand = new Command(async () =>
                
                await Shell.Current.GoToAsync("//transaction-form")
           );
                    
            //LoadTransactions();
            
        }

        //---------------------------Load tranasaction from API
        //used  for edit mode

        public async Task LoadTransactions()
        {
            
            try
            {
                //Get data from backend

            var transactions = await _transactionService.GetTransactionAsync();

              // Clear old items
                Transactions.Clear();

               //Add new items
               foreach(var transaction in transactions)
               {
                    Transactions.Add(transaction);
                }
            }
           catch(Exception ex)
            {
               await Application.Current.MainPage.DisplayAlert(
                "Error",
               ex.Message,
               "OK");
            }
           
        }
        //--------------------------------Edit transaction
       private async Task EditTransaction(TransactionDto transaction)
        {
            //Store selected transaction
            TemporaryData.selectedTranaction = transaction;

            //Navigate to form page
            await Shell.Current.GoToAsync("//transaction-form");
        }

        //--------------------------------Delete transaction  
        private async Task DeleteTransaction(Guid id)
        {
            try
            {
                //Delete from backend
                await _transactionService.DeleteTransactionAsync(id);

                //Reload list
                LoadTransactions();

            }
            catch (Exception ex)
            {
                        await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    ex.Message,
                    "OK");
            }
        }

    }
}
