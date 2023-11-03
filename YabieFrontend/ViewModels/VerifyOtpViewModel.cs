using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using YabieFrontend.IServices;
using YabieFrontend.UI.Pages;

namespace YabieFrontend.ViewModels;

public partial class VerifyOtpViewModel : ObservableObject, IQueryAttributable
{
    private readonly IAlertService _alertService;
    private readonly IAuthenticationService _authenticationService;
    [ObservableProperty] private string _phoneNumber;
    [ObservableProperty] private string _otp1;
    [ObservableProperty] private string _otp2;
    [ObservableProperty] private string _otp3;
    [ObservableProperty] private string _otp4;
    [ObservableProperty] private string _otp5;
    [ObservableProperty] private string _otp6;

    public VerifyOtpViewModel(IAlertService alertService, IAuthenticationService authenticationService)
    {
        _alertService = alertService;
        _authenticationService = authenticationService;
    }

    [RelayCommand]
    private async Task VerityOtp()
    {
        if (string.IsNullOrWhiteSpace(PhoneNumber) || PhoneNumber.Length < 10)
        {
            await _alertService.ShowAlertAsync("Error", "Please enter valid a phone number");
            return;
        }

        var otp = Otp1 + Otp2 + Otp3 + Otp5 + Otp6;
        var isOtpValid = await _authenticationService.VerifyOtp(PhoneNumber, otp);
        if (!isOtpValid)
        {
            await _alertService.ShowAlertAsync("Error", "Wrong Otp. Please try again");
            return;
        }

        await Shell.Current.GoToAsync($"///{nameof(CreateUserPage)}");
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        try
        {
            PhoneNumber = query["PhoneNumber"] as string ?? string.Empty;
        }
        catch (Exception)
        {
            PhoneNumber = "9021787292";
        }
    }
}