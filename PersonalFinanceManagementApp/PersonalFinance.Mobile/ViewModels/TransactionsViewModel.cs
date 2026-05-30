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

        //CATEGORIES FOR SEARCH
        private readonly CategoryService _categoryService;



        private List<CategoryDto> _categories = [];

        //Collection automatically updates UI
        public ObservableCollection<TransactionDto> Transactions { get; set; }

        //Original  transactions
        private List<TransactionDto> _allTransactions = new();


        //Command for deleting transaction
        public ICommand DeleteCommand { get; }

        //Command for  adding transaction
        public ICommand AddCommand { get; }

        //Command for editing
        public ICommand EditCommand { get; }

        //Show 3 .dot
        public ICommand ShowOptionsCommand { get; }

        //----------------------search command
        public ICommand SearchCommand { get; }
        public ICommand AllFilterCommand { get; }

        public ICommand IncomeFilterCommand { get; }

        public ICommand ExpenseFilterCommand { get; }

        //SEARCH && FILTER
       private string _searchText;

        public string SearchText
        {
            get => _searchText;
            set
            {
               _searchText = value;
               OnPropertyChanged();
              ApplySearch();
           }
        }

        private string _selectedFilter = "All";

        public string SelectedFilter
        {
            get => _selectedFilter;
            set
            {
                _selectedFilter = value;
                OnPropertyChanged();

                ApplyFilters();
            }
        }

        public TransactionsViewModel()
        {
            _transactionService = new TransactionService();
            _categoryService = new CategoryService();
            Transactions = new ObservableCollection<TransactionDto>();

            EditCommand =new Command<TransactionDto>( async (transaction) =>await EditTransaction(transaction));

            DeleteCommand = new Command<Guid>(async (id) => await DeleteTransaction(id));

            ShowOptionsCommand = new Command<TransactionDto>(async transaction =>
            {
                string action =
                    await Application.Current.MainPage
                        .DisplayActionSheet(
                            "Options",
                            "Cancel",
                            null,
                            "Edit",
                            "Delete");

                if (action == "Edit")
                {
                    EditCommand.Execute(transaction);
                }
                else if (action == "Delete")
                {
                    DeleteCommand.Execute(transaction);
                }
            });



            AddCommand = new Command(async () =>await Shell.Current.GoToAsync("transaction-form") );

            SearchCommand =new Command(ApplyFilters);
            AllFilterCommand =new Command(() => SelectedFilter = "All");

            IncomeFilterCommand = new Command(() => SelectedFilter = "Income");

            ExpenseFilterCommand = new Command(() => SelectedFilter = "Expense");

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
                _allTransactions = transactions.ToList();

                //Get categories
                _categories =(await _categoryService.GetCategoriesAsync())
                    .ToList();

                // Clear old items
                Transactions.Clear();

               //Add new items
               foreach(var transaction in _allTransactions)
               {
                    transaction.Icon =
                        CategoryIconHelper.GetIcon(
                            transaction.CategoryName);
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
            TemporaryData.selectedTransaction = transaction;

            //Navigate to form page
            await Shell.Current.GoToAsync("transaction-form");
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

        //----------SEARCH BAR
        private void ApplySearch()
        {
            Transactions.Clear();

            var filtered =
                _allTransactions.Where(t =>
                    string.IsNullOrWhiteSpace(SearchText)

                    ||

                    t.Description.Contains(
                        SearchText,
                        StringComparison.OrdinalIgnoreCase)

                    ||
                    t.Amount.ToString().Contains(
                        SearchText,
                        StringComparison.OrdinalIgnoreCase)
                    ||

                    t.CategoryName.Contains(
                        SearchText,
                        StringComparison.OrdinalIgnoreCase));

            foreach (var transaction in filtered)
            {
                Transactions.Add(transaction);
            }
        }
        //Filter
        private void ApplyFilters()
        {
            Transactions.Clear();

            var filtered =
                _allTransactions.Where(t =>
                    string.IsNullOrWhiteSpace(SearchText)
                    ||
                    t.Description.Contains(
                        SearchText,
                        StringComparison.OrdinalIgnoreCase)
                    ||
                    t.CategoryName.Contains(
                        SearchText,
                        StringComparison.OrdinalIgnoreCase));

            // FILTER BUTTONS

            if (SelectedFilter != "All")
            {
                filtered = filtered.Where(t =>
                {
                    var category =
                        _categories.FirstOrDefault(
                            c => c.Id == t.CategoryId);

                    return category?.Type == SelectedFilter;
                });
            }

            foreach (var transaction in filtered)
            {
                Transactions.Add(transaction);
            }
        }




    }
}
