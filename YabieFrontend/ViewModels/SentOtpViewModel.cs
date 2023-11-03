using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using YabieFrontend.IServices;
using YabieFrontend.UI.Pages;

namespace YabieFrontend.ViewModels;

public partial class SentOtpViewModel : ObservableObject
{
    private readonly IAlertService _alertService;
    private readonly IAuthenticationService _authenticationService;
    [ObservableProperty] private string _phoneNumber;

    public SentOtpViewModel(IAlertService alertService, IAuthenticationService authenticationService)
    {
        _alertService = alertService;
        _authenticationService = authenticationService;
    }

    [RelayCommand]
    private async Task SendOtp()
    {
        if (string.IsNullOrWhiteSpace(PhoneNumber) || PhoneNumber.Length < 10)
        {
            await _alertService.ShowAlertAsync("Error", "Please enter valid a phone number");
            return;
        }

        var isOtpSent = await _authenticationService.SendOtp(PhoneNumber);
        if (!isOtpSent)
        {
            await _alertService.ShowAlertAsync("Error", "Could not sent otp. Please enter valid phone number.");
            return;
        }

        await Shell.Current.GoToAsync($"///{nameof(CreateUserPage)}");
    }
}