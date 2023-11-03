using YabieFrontend.IServices;
using YabieFrontend.ViewModels;

namespace YabieFrontend.UI.Views;

public partial class ItemListView
{
    public ItemListView()
    {
        InitializeComponent();
        var itemListViewModel = MauiProgram.GetService<ItemListViewModel>();
        BindingContext = itemListViewModel;
        InitializeAsync(itemListViewModel);
    }

    private async void InitializeAsync(ItemListViewModel itemListViewModel)
    {
        var productService = MauiProgram.GetService<IProductService>();

        var products = await productService.ListAllProducts();

        // Update the Items collection
        itemListViewModel.Items.Clear();
        foreach (var product in products)
        {
            itemListViewModel.Items.Add(product);
        }
    }
}