using System.Windows.Input;

using PersonalFinance.Mobile.Services;

using PersonalFinance.Shared.DTOs.Profile;

namespace PersonalFinance.Mobile.ViewModels;

public class ProfileViewModel : BaseViewModel
{
    private readonly ProfileService _profileService;

    // Name
    private string _name;

    public string Name
    {
        get => _name;

        set
        {
            _name = value;
            OnPropertyChanged();
        }
    }

    // Surname
    private string _surName;

    public string SurName
    {
        get => _surName;

        set
        {
            _surName = value;
            OnPropertyChanged();
        }
    }

    // Email
    private string _email;

    public string Email
    {
        get => _email;

        set
        {
            _email = value;
            OnPropertyChanged();
        }
    }

    // Save command
    public ICommand SaveCommand { get; }

    public ProfileViewModel()
    {
        _profileService =
            new ProfileService();

        SaveCommand =
            new Command(async () => await Save());

        _ = LoadProfile();
    }

    // Load user profile
    private async Task LoadProfile()
    {
        try
        {
            var profile =await _profileService.GetProfileAsync();

            Name = profile.Name;

            SurName = profile.SurName;

            Email = profile.Email;
        }
        catch (Exception ex)
        {
            await Application.Current.MainPage
                .DisplayAlert(
                    "Error",
                    ex.Message,
                    "OK");
        }
    }

    // Save profile
    private async Task Save()
    {
        try
        {
            var request =
                new UpdateProfileRequestDto
                {
                    Name = Name,
                    SurName = SurName,
                    Email = Email
                };

            await _profileService .UpdateProfileAsync(request);

            await Application.Current.MainPage
                .DisplayAlert(
                    "Success",
                    "Profile updated",
                    "OK");
        }
        catch (Exception ex)
        {
            await Application.Current.MainPage
                .DisplayAlert(
                    "Error",
                    ex.Message,
                    "OK");
        }
    }
}