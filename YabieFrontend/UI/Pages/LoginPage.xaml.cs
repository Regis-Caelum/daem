using Android.Widget;
using CommunityToolkit.Maui.Core.Platform;
using YabieFrontend.ViewModels;

namespace YabieFrontend.UI.Pages;

public partial class LoginPage
{
    public LoginPage(LoginViewModel loginViewModel)
    {
        InitializeComponent();
        Shell.SetNavBarIsVisible(this, false);
        BindingContext = loginViewModel;
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        PasswordEntry.HideKeyboardAsync(new CancellationToken(false));
        PhoneNumberEntry.HideKeyboardAsync(new CancellationToken(false));
    }


    protected override bool OnBackButtonPressed()
    {
        Application.Current?.Quit();
        Toast.MakeText(MauiApplication.Current, "success2", ToastLength.Long)?.Show();
        return true;
    }

    private async void TapGestureRecognizer_OnTapped(object sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync($"{nameof(SendOtpPage)}");
    }
}