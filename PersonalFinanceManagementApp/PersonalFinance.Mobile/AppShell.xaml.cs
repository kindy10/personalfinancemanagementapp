using PersonalFinance.Mobile.Views;

namespace PersonalFinance.Mobile
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            //await Shell.Current.GoToAsync("//login");

            Routing.RegisterRoute(
                "transaction-form",
                typeof(TransactionFormPage));

            Routing.RegisterRoute(
                "category-form",
                 typeof(CategoryFormPage));

            Routing.RegisterRoute(
                "budget-form",
                typeof(BudgetFormPage));
        }
    }
}
