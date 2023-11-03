using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using YabieFrontend.IServices;
using YabieFrontend.Models;
using YabieFrontend.UI.Pages;

namespace YabieFrontend.ViewModels;

public partial class AuctionListViewModel : ObservableObject
{
    [ObservableProperty] private ObservableCollection<Auction> _auctions = new();

    public AuctionListViewModel()
    {
        var auctionService = MauiProgram.GetService<IAuctionService>();

        Auctions.Clear();
        var auctions = auctionService.ListAllAuctions().GetAwaiter().GetResult();
        foreach (var auction in auctions)
        {
            Auctions.Add(auction);
        }
    }

    [RelayCommand]
    private async Task LoadBiddingPage(Auction auction)
    {
        await Shell.Current.GoToAsync($"{nameof(AuctionPage)}?", new Dictionary<string, object>
        {
            ["Id"] = auction.AuctionId
        });
    }
}