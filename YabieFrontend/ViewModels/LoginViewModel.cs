using Android.Widget;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using YabieFrontend.IServices;

namespace YabieFrontend.ViewModels;

public partial class LoginViewModel : ObservableObject
{
    private readonly IAuthenticationService _authenticationService;
    private readonly IAlertService _alertService;
    [ObservableProperty] private string _phoneNumber;
    [ObservableProperty] private string _password;

    public LoginViewModel(IAuthenticationService authenticationService, IAlertService alertService)
    {
        _authenticationService = authenticationService;
        _alertService = alertService;
    }


    [RelayCommand]
    private async Task Login()
    {
        Toast.MakeText(MauiApplication.Current, "success", ToastLength.Long)?.Show();
        if (string.IsNullOrWhiteSpace(PhoneNumber) || string.IsNullOrWhiteSpace(Password))
        {
            await _alertService.ShowAlertAsync("Error", "Phone number and password cannot be empty");
            return;
        }

        if (PhoneNumber.Length < 10)
        {
            await _alertService.ShowAlertAsync("Error", "Please enter valid a phone number");
            return;
        }

        if (Password.Length < 8)
        {
            await _alertService.ShowAlertAsync("Error", "Please enter a valid password");
            return;
        }

        if (PhoneNumber.Equals("9021787292") && Password.Equals("11111111"))
        {
            await Shell.Current.GoToAsync("DashboardPage");
            PhoneNumber = string.Empty;
            Password = string.Empty;
        }
    }
}