using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using YabieFrontend.Models;
using YabieFrontend.UI.Pages;

namespace YabieFrontend.ViewModels;

public partial class ItemListViewModel : ObservableObject
{
    // [ObservableProperty] private string _catalogImageUrl;
    // [ObservableProperty] private string _productTitle;
    // [ObservableProperty] private string _startingPrice;
    // [ObservableProperty] private string _originalPrice;
    // [ObservableProperty] private string _savedMoney;
    [ObservableProperty] private ObservableCollection<Product> _items = new();

    [RelayCommand]
    private static async Task LoadItemPage(Product product)
    {
        await Shell.Current.GoToAsync($"{nameof(ItemPage)}?", new Dictionary<string, object>
        {
            ["Product"] = product
        });
    }
}