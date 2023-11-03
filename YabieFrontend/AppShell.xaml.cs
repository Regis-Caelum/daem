using YabieFrontend.UI.Pages;

namespace YabieFrontend;

public partial class AppShell
{
    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
        Routing.RegisterRoute(nameof(DashboardPage), typeof(DashboardPage));
        Routing.RegisterRoute(nameof(ItemPage), typeof(ItemPage));
        Routing.RegisterRoute(nameof(AuctionGuidelinesPage), typeof(AuctionGuidelinesPage));
        Routing.RegisterRoute(nameof(AuctionPage), typeof(AuctionPage));
        Routing.RegisterRoute(nameof(SendOtpPage), typeof(SendOtpPage));
        Routing.RegisterRoute(nameof(VerifyOtpPage), typeof(VerifyOtpPage));
    }
}