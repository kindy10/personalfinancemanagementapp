using System.Windows.Input;

using PersonalFinance.Mobile.Services;

using PersonalFinance.Shared.DTOs.Profile;

namespace PersonalFinance.Mobile.ViewModels;

public class ProfileViewModel : BaseViewModel
{
    private readonly ProfileService _profileService;
    private readonly TransactionService _transactionService;
    private readonly BudgetService _budgetService;
    private readonly CategoryService _categoryService;

    //For statistic card
    public int TransactionsCount { get; set; }

    public int BudgetsCount { get; set; }

    public int CategoriesCount { get; set; }

    // Name
    private string _name;

    public string Name
    {
        get => _name;

        set
        {
            _name = value;
            OnPropertyChanged();
        }
    }

    // Surname
    private string _surName;

    public string SurName
    {
        get => _surName;

        set
        {
            _surName = value;
            OnPropertyChanged();
        }
    }

    // Email
    private string _email;

    public string Email
    {
        get => _email;

        set
        {
            _email = value;
            OnPropertyChanged();
        }
    }

    // Save command
    public ICommand SaveCommand { get; }

    //Logout Command
    public ICommand LogoutCommand { get; }

    //ChangePassword Command
    public ICommand ChangePasswordCommand { get; }

    public ProfileViewModel()
    {
        _profileService =
            new ProfileService();

        SaveCommand =
            new Command(async () => await Save());
        LogoutCommand =new Command(async () => await Logout());

        ChangePasswordCommand =
                new Command(async () =>
                    await Shell.Current.GoToAsync("auth-changePassword"));

        _transactionService = new TransactionService();
        _budgetService = new BudgetService();
        _categoryService = new CategoryService();

        _ = LoadProfile();
    }

    // Load user profile
    private async Task LoadProfile()
    {
        try
        {
            var profile =await _profileService.GetProfileAsync();

            Name = profile.Name;

            SurName = profile.SurName;

            Email = profile.Email;
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

    // Save profile
    private async Task Save()
    {
        try
        {
            var request =
                new UpdateProfileRequestDto
                {
                    Name = Name,
                    SurName = SurName,
                    Email = Email
                };

            await _profileService .UpdateProfileAsync(request);

            await Application.Current.MainPage
                .DisplayAlert(
                    "Success",
                    "Profile updated",
                    "OK");
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
    //Logout 
    private async Task Logout()
    {
        bool confirm =
            await Application.Current.MainPage.DisplayAlert(
                "Logout",
                "Are you sure you want to logout?",
                "Yes",
                "No");

        if (!confirm)
            return;

        // Clear saved token

        SecureStorage.Remove("auth_token");

        await Shell.Current.GoToAsync("//login");
    }

    public async Task LoadStatistics()
    {
        try
        {
            var transactions =
                await _transactionService.GetTransactionAsync();

            var budgets =
                await _budgetService.GetBudgetsAsync();

            var categories =
                await _categoryService.GetCategoriesAsync();

            TransactionsCount = transactions.Count();

            BudgetsCount = budgets.Count();

            CategoriesCount = categories.Count();

            OnPropertyChanged(nameof(TransactionsCount));
            OnPropertyChanged(nameof(BudgetsCount));
            OnPropertyChanged(nameof(CategoriesCount));
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