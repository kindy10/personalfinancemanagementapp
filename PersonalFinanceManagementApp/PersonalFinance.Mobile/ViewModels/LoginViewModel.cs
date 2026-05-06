using PersonalFinance.Mobile.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PersonalFinance.Mobile.ViewModels
{
    public  class LoginViewModel
    {
        private readonly AuthService _authService;

        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        public ICommand LoginCommand { get; }
        
        public LoginViewModel()
        {
            _authService = new AuthService();
            LoginCommand = new Command(async () => await Login());
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
