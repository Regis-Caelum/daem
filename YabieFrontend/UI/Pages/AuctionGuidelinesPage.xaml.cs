namespace YabieFrontend.UI.Pages;

public partial class AuctionGuidelinesPage
{
    public AuctionGuidelinesPage()
    {
        InitializeComponent();
    }

    private void Button_OnPressed(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync($"../{nameof(AuctionPage)}");
    }
}