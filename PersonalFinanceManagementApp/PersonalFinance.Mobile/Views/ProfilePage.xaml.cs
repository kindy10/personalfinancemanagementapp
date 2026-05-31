using PersonalFinance.Mobile.ViewModels;

namespace PersonalFinance.Mobile.Views;

public partial class ProfilePage : ContentPage
{
    public ProfilePage()
    {
        InitializeComponent();

        BindingContext =
            new ProfileViewModel();
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is ProfileViewModel vm)
        {
            await vm.LoadStatistics();
        }
    }
}