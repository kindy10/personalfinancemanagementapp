using System.Globalization;

namespace PersonalFinance.Mobile.Converters;

public class BudgetStatusColorConverter : IValueConverter
{
    public object Convert(
        object value,
        Type targetType,
        object parameter,
        CultureInfo culture)
    {
        var status = value?.ToString();

        return status switch
        {
            "Safe" =>
                Application.Current.Resources["SuccessColor"],

            "Warning" =>
                Application.Current.Resources["WarningColor"],

            "Danger" =>
                Application.Current.Resources["DangerColor"],

            _ =>
                Colors.Gray
        };
    }

    public object ConvertBack(
        object value,
        Type targetType,
        object parameter,
        CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}