using PersonalFinance.Mobile.Services;
using System;
using System.Windows.Input;

namespace PersonalFinance.Mobile.ViewModels
{
    public  class LoginViewModel
    {
        private readonly AuthService _authService;

        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        public ICommand LoginCommand { get; }

        public ICommand GoToRegisterCommand { get; }

        public LoginViewModel()
        {
            _authService = new AuthService();
            LoginCommand = new Command(async () => await Login());
            GoToRegisterCommand =new Command(async () =>
                                    await Shell.Current.GoToAsync("//register"));
        }

        private async Task Login()
        {
            try
            {
                 var result = await _authService.LoginAsync(Email, Password);

                await Application.Current.MainPage.DisplayAlert(
                    "Success",
                    $"Welcome {result.Name}",
                    "OK");

                //Navigate after login
                await Shell.Current.GoToAsync("//dashboard");
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }
    }
}
