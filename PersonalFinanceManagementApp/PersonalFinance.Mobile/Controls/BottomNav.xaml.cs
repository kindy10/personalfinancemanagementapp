using PersonalFinance.Mobile.Views;

namespace PersonalFinance.Mobile.Controls;

public partial class BottomNav : ContentView
{
    public BottomNav()
    {
        InitializeComponent();
    }

    private async void OnDashboardClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//dashboard");
    }

    private async void OnTransactionsClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//transactions");
    }

    private async void OnAddClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(TransactionFormPage));
    }

    private async void OnBudgetsClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//budgets");
    }

    private async void OnProfileClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//profile");
    }
}