using PersonalFinance.Mobile.Services;
using PersonalFinance.Shared.DTOs.Categories;
using PersonalFinance.Shared.DTOs.Transactions;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace PersonalFinance.Mobile.ViewModels
{
    public class TransactionFormViewModel : BaseViewModel
    {
        private readonly TransactionService _transactionService;
        private readonly CategoryService _categoryService;
        // Transaction ID (used only in edit mode)
        private Guid _transactionId;

        // Determines create or edit mode
        public bool IsEditMode { get; set; }

        // Form fields
        private string _amount = string.Empty;

        private string _description = string.Empty;

        private DateTime _date = DateTime.Now;

        private CategoryDto _selectedCategory;

        // Categories list
        public ObservableCollection<CategoryDto> Categories { get; set; }

        // Bindable properties
        public string Amount
        {
            get => _amount;
            set
            {
                _amount = value;
                OnPropertyChanged();
            }
        }

        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                OnPropertyChanged();
            }
        }

        public DateTime Date
        {
            get => _date;
            set
            {
                _date = value;
                OnPropertyChanged();
            }
        }

        public CategoryDto SelectedCategory
        {
            get => _selectedCategory;
            set
            {
                _selectedCategory = value;
                OnPropertyChanged();
            }
        }

        //-----Update page title dynamically ----//
        private string _pageTitle = "Add Transaction";

        public string PageTitle
        {
            get => _pageTitle;

            set
            {
                _pageTitle = value;
                OnPropertyChanged();
            }
        }
        // Save command
        public ICommand SaveCommand { get; }

        public TransactionFormViewModel()
        {
            _transactionService = new TransactionService();

            _categoryService = new CategoryService();

            Categories =
                new ObservableCollection<CategoryDto>();

            SaveCommand =
                new Command(async () => await Save());


           // LoadCategoriesAsync();

            
        }

        // Used for edit mode
        public async Task LoadTransactionAsync(TransactionDto transaction)
        {
            await LoadCategoriesAsync();

            PageTitle = "Edit Transaction";
            IsEditMode = true;

            _transactionId = transaction.Id;

            Amount = transaction.Amount.ToString();
            Description = transaction.Description;
            Date = transaction.Date;

            SelectedCategory =Categories.FirstOrDefault( c => c.Id == transaction.CategoryId);
        }
        public async Task LoadCategoriesAsync()
        {
            try
            {
                //Get categories from backend
                var categories = await _categoryService.GetCategoriesAsync();

                //Clear existing items
                Categories.Clear();

                //Add categories to observable collection
                foreach (var category in categories)
                    Categories.Add(category);
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage
                    .DisplayAlert(
                        "Error",
                        ex.Message,
                        "OK");
            }
        }

        // Save transaction
        private async Task Save()
        {
            try
            {
                // Basic validation
                if (!decimal.TryParse(Amount, out decimal amount))
                {
                    await Application.Current.MainPage
                        .DisplayAlert(
                            "Validation",
                            "Invalide amount",
                            "OK");

                    return;
                }
                if (amount <= 0)
                {
                    await Application.Current.MainPage.DisplayAlert(
                       "Validation",
                       "Amount must be greater than zero",
                       "OK");

                    return;
                }

                if (SelectedCategory == null)
                {
                    await Application.Current.MainPage
                        .DisplayAlert(
                            "Validation",
                            "Please select a category",
                            "OK");

                    return;
                }
                // CREATE MODE
                if (!IsEditMode)
                {
                    var request =
                        new CreateTransactionRequestDto
                        {
                            Amount = amount,
                            Description = Description,
                            Date = Date,
                            CategoryId =
                                SelectedCategory.Id
                        };

                    await _transactionService
                        .CreateTransactionAsync(
                            request);
                }

                // EDIT MODE
                else
                {
                    var request =
                        new UpdateTransactionRequestDto
                        {
                            Amount = amount,
                            Description = Description,
                            Date = Date,
                            CategoryId =
                                SelectedCategory.Id
                        };
                    await _transactionService
                   .UpdateTransactionAsync(
                       _transactionId,
                       request);
                }

                // Success message
                await Application.Current.MainPage
                    .DisplayAlert(
                        "Success",
                        "Transaction saved",
                        "OK");

                // Return to transactions page
                await Shell.Current.GoToAsync("..");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

                await Application.Current.MainPage.DisplayAlert(
                    "FULL ERROR",
                    ex.ToString(),
                    "OK");
            }
        }
    }
}
