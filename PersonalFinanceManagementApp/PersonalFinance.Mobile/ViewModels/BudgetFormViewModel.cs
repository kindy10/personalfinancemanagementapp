
using System.Collections.ObjectModel;
using System.Windows.Input;
using PersonalFinance.Mobile.Services;
using PersonalFinance.Shared.DTOs.Budgets;
using PersonalFinance.Shared.DTOs.Categories;

namespace PersonalFinance.Mobile.ViewModels;
public class BudgetFormViewModel : BaseViewModel
{
    private readonly BudgetService _budgetService;

    private readonly CategoryService _categoryService;

    // Budget ID for edit mode
    private Guid _budgetId;

    // Determines create/update
    public bool IsEditMode { get; set; }

    // Page title
    private string _pageTitle = "Add Budget";

    public string PageTitle
    {
        get => _pageTitle;

        set
        {
            _pageTitle = value;
            OnPropertyChanged();
        }
    }

    // Amount input
    private string _amount;

    public string Amount
    {
        get => _amount;

        set
        {
            _amount = value;
            OnPropertyChanged();
        }
    }

    // Selected month
    private DateTime _month = DateTime.Now;

    public DateTime Month
    {
        get => _month;

        set
        {
            _month = value;
            OnPropertyChanged();
        }
    }

    // Selected category
    private CategoryDto _selectedCategory;

    public CategoryDto SelectedCategory
    {
        get => _selectedCategory;

        set
        {
            _selectedCategory = value;
            OnPropertyChanged();
        }
    }
    // Categories list
    public ObservableCollection<CategoryDto>
        Categories
    { get; set; }

    // Save command
    public ICommand SaveCommand { get; }

    public BudgetFormViewModel()
    {
        _budgetService =
            new BudgetService();

        _categoryService =
            new CategoryService();

        Categories =
            new ObservableCollection<CategoryDto>();

        SaveCommand =
            new Command(async () => await Save());

        //“I intentionally ignore the returned Task.”
        /*  if Because this call is not awaited,
            execution continues before the call completes
         */

        //_ = LoadCategoriesAsync();
    }

    // Load categories
    private async Task LoadCategoriesAsync()
    {
        try
        {
            var categories =
                await _categoryService
                    .GetCategoriesAsync();

            Categories.Clear();

            foreach (var category in categories)
            {
                Categories.Add(category);
            }
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

    // Load existing budget for edit
    public async Task LoadBudgetAsync(BudgetDto budget)
    {
        await LoadCategoriesAsync();
        IsEditMode = true;

        _budgetId = budget.Id;

        Amount = budget.MonthlyLimit.ToString();

        Month = budget.Month;

        PageTitle = "Edit Budget";
        SelectedCategory = Categories.FirstOrDefault(c => c.Id == budget.CategoryId);

    }

    // Save budget
    private async Task Save()
    {
        try
        {
            // Validate amount
            if (!decimal.TryParse(
                    Amount,
                    out var amount))
            {
                await Application.Current.MainPage
                    .DisplayAlert(
                        "Validation",
                        "Invalid amount",
                        "OK");

                return;
            }

            // Validate category
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
                    new CreateBudgetRequestDto
                    {
                        MonthlyLimit = amount,
                        Month = Month,
                        CategoryId = SelectedCategory.Id
                    };

                await _budgetService
                    .CreateBudgetAsync(request);
            }

            // EDIT MODE
            else
            {
                var request =
                    new UpdateBudgetRequestDto
                    {
                        MonthlyLimit = amount,
                        Month = Month,
                        CategoryId = SelectedCategory.Id
                    };

                await _budgetService
                    .UpdateBudgetAsync(
                        _budgetId,
                        request);
            }

            await Application.Current.MainPage
                .DisplayAlert(
                    "Success",
                    "Budget saved",
                    "OK");

            await Shell.Current.GoToAsync("..");
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
}