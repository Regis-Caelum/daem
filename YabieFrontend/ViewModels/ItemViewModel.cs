using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using YabieFrontend.IServices;
using YabieFrontend.Models;
using YabieFrontend.UI.Pages;

namespace YabieFrontend.ViewModels;

public partial class ItemViewModel : ObservableObject, IQueryAttributable
{
    [ObservableProperty] private Product _product;
    [ObservableProperty] private FormattedString _formattedDescription;

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        Product = (Product)query["Product"];
        // Split the description into lines
        var lines = Product.Description.Split('\n');
        var formattedString = new FormattedString();

        foreach (var line in lines)
        {
            formattedString.Spans.Add(new Span { Text = "• ", FontAttributes = FontAttributes.Bold });
            formattedString.Spans.Add(new Span { Text = line });
            formattedString.Spans.Add(new Span { Text = "\n" });
        }

        FormattedDescription = formattedString;
    }

    [RelayCommand]
    private async Task LoadAuctionDisclaimerPage()
    {
        await Shell.Current.GoToAsync($"{nameof(AuctionGuidelinesPage)}", new Dictionary<string, object>
        {
            ["Id"] = Product.ProductId
        });
    }
}