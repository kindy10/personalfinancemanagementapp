using PersonalFinance.Mobile.ViewModels;

namespace PersonalFinance.Mobile.Views;

public partial class DashboardPage : ContentPage
{
	public DashboardPage()
	{
		InitializeComponent();

		//Connect ViewModel to page
		BindingContext = new DashboardViewModel();
	}
    //Execute automatically every time the Dasboard Page becomes visible
    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is DashboardViewModel vm)
        {
            await vm.LoadData();
        }
    }
}