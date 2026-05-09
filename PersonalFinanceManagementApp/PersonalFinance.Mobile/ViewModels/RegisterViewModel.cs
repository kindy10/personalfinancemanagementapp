using PersonalFinance.Mobile.Services;
using PersonalFinance.Shared.DTOs.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PersonalFinance.Mobile.ViewModels
{
    public  class RegisterViewModel :BaseViewModel
    {
        private readonly AuthService _authService;

        //Properties bound to UI

        private string _name = string.Empty;
        private string _surName = string.Empty;
        private string _email = string.Empty;
        private string _password = string.Empty;

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnpropertyChanged();
            }
        }
        public string SurnName
        {
            get => _surName;
            set
            {
                _surName = value;
                OnpropertyChanged();
            }
        }
        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnpropertyChanged();
            }
        }
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnpropertyChanged();
            }
        }

        //Commands
        public ICommand RegisterCommand { get; }
        public ICommand GoToLoginCommnand { get; }

        public RegisterViewModel()
        {
            _authService = new AuthService();

            RegisterCommand = new Command(async () => await Register());

            GoToLoginCommnand = new Command(async () => await Shell.Current.GoToAsync("//login"));
        }

        private async Task Register()
        {
            try
            {
                //Build request DTO
                var request = new RegisterRequestDto
                {
                    Name = Name,
                    SurName = SurnName,
                    Email = Email,
                    Password = Password,
                };

                //Call API
                var result = await _authService.RegisterAsync(request);

                //Navigate to dashboard

                await Shell.Current.GoToAsync("//dashboard");
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


}
