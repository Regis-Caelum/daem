using YabieBackend.Models;
using YabieFrontend.IServices;
using YabieFrontend.Models;
using YabieFrontend.ViewModels;

namespace YabieFrontend.UI.Pages;

public partial class AuctionListPage
{
    public AuctionListPage(AuctionListViewModel auctionListViewModel)
    {
        InitializeComponent();
        BindingContext = auctionListViewModel;
        InitializeAsync(auctionListViewModel);
    }

    private static async void InitializeAsync(AuctionListViewModel auctionListViewModel)
    {
        var auctionService = MauiProgram.GetService<IAuctionService>();

        var auctions = await auctionService.ListAllAuctions();

        // Update the Auctions collection
        auctionListViewModel.Auctions.Clear();
        foreach (var auction in auctions)
        {
            auctionListViewModel.Auctions.Add(auction);
        }

        if (auctionListViewModel.Auctions.Count == 0)
        {
            auctionListViewModel.Auctions.Add(new Auction
            {
                AuctionId = "testId",
                Status = Status.OPEN,
                EndTime = DateTime.Now,
                RegistrationFee = 500,
                StartTime = DateTime.Now,
                ProductRefId = "https://media.stockinthechannel.com/pic/9dxjoV1nYk2pQadv-cb2kg.c-r.jpgProduct%20Image"
            });
        }
    }
}