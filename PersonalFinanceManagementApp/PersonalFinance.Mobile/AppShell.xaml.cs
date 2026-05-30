using PersonalFinance.Mobile.Views;

namespace PersonalFinance.Mobile
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            CheckAuthentication();

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
        private async void CheckAuthentication()
        {
            try
            {
                var token =
                    await SecureStorage.GetAsync(
                        "auth_token");

                if (string.IsNullOrWhiteSpace(token))
                {
                    await GoToAsync("//login");
                }
                else
                {
                    await GoToAsync("//dashboard");
                }
            }
            catch
            {
                await GoToAsync("//login");
            }
        }
    }
}
