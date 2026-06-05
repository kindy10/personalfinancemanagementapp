using PersonalFinance.Mobile.Services;
using PersonalFinance.Shared.DTOs.Auth;
using PersonalFinance.Mobile.Helpers;
using System.Windows.Input;

namespace PersonalFinance.Mobile.ViewModels
{
    public  class ChangePasswordViewModel
    {
        private readonly AuthService _authService;
        public string CurrentPassword { get; set; }

        public string NewPassword { get; set; }

        public string ConfirmPassword { get; set; }

        //Command
        public ICommand ChangePasswordCommand { get; }

        public ChangePasswordViewModel()
        {
            _authService = new AuthService();

            ChangePasswordCommand =new Command(async () => await ChangePassword());
        }

        private async Task ChangePassword()
        {
            if (string.IsNullOrWhiteSpace(CurrentPassword))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Validation",
                    "Current password is required",
                    "OK");

                return;
            }
            if (NewPassword != ConfirmPassword)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Validation",
                    "Passwords does not match",
                    "OK");

                return;
            }

            //---CALL THE API
            try
            {
                var request = new ChangePasswordRequestDto
                {
                    CurrentPassword = CurrentPassword,
                    NewPassword = NewPassword,
                    ConfirmPassword = ConfirmPassword
                };

                await _authService.ChangePasswordAsync(
                    request);
                await Application.Current.MainPage.DisplayAlert(
                    "Success",
                    "Password changed successfully",
                    "OK");

                //----GO BACK
                await Shell.Current.GoToAsync("..");
            }
            catch(Exception ex)
            {
                await AlertHelper.ShowErrorAsync(
                    ex.Message);
            }
        }
    }
}
