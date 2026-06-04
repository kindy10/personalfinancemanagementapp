namespace PersonalFinance.Mobile.Helpers;
public static class AlertHelper
{
    public static async Task ShowErrorAsync(string message)
    {
        await Application.Current.MainPage.DisplayAlert(
            "Error",
            message,
            "OK");
    }

    public static async Task ShowSuccessAsync(string message)
    {
        await Application.Current.MainPage.DisplayAlert(
            "Success",
            message,
            "OK");
    }

    public static async Task ShowValidationAsync(string message)
    {
        await Application.Current.MainPage.DisplayAlert(
            "Validation",
            message,
            "OK");
    }

    public static async Task<bool> ShowConfirmationAsync(
        string title,
        string message)
    {
        return await Application.Current.MainPage.DisplayAlert(
            title,
            message,
            "Yes",
            "No");
    }
}