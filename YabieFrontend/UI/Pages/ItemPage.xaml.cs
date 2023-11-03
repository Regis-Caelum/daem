using YabieFrontend.ViewModels;

namespace YabieFrontend.UI.Pages;

public partial class ItemPage
{
    public ItemPage(ItemViewModel itemViewModel)
    {
        InitializeComponent();
        BindingContext = itemViewModel;
    }
}