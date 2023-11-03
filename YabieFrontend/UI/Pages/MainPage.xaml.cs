namespace YabieFrontend.UI.Pages;

public partial class MainPage
{
    public MainPage()
    {
        InitializeComponent();

        // Call the async method to create a delay of 2 seconds and navigate to the login page
        NavigateToLoginPageAsync();
    }
    
    private async void NavigateToLoginPageAsync()
    {
        // Create a delay of 2 seconds
        await Task.Delay(1000);

        // Navigate to the login page
        await Shell.Current.GoToAsync(nameof(LoginPage));
        // await Shell.Current.GoToAsync($"///{nameof(AuctionListPage)}");
    }

}