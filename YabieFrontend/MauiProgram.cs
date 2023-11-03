using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using YabieFrontend.IServices;
using YabieFrontend.Services;
using YabieFrontend.UI.Pages;
using YabieFrontend.ViewModels;

namespace YabieFrontend;

public static class MauiProgram
{
    private static IServiceProvider _serviceProvider;

    public static TService GetService<TService>()
        => _serviceProvider.GetService<TService>();
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        // Services
        builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
        builder.Services.AddScoped<IAlertService, AlertService>();
        builder.Services.AddSingleton<IProductService, ProductService>();
        builder.Services.AddSingleton<IAuctionService, AuctionService>();

        // Pages
        builder.Services.AddTransient<LoginPage>();
        builder.Services.AddTransient<DashboardPage>();
        builder.Services.AddTransient<ItemPage>();
        builder.Services.AddTransient<AuctionListPage>();
        builder.Services.AddTransient<AuctionPage>();
        builder.Services.AddTransient<SendOtpPage>();
        builder.Services.AddTransient<VerifyOtpPage>();

        // View Models
        builder.Services.AddTransient<LoginViewModel>();
        builder.Services.AddTransient<SentOtpViewModel>();
        builder.Services.AddTransient<VerifyOtpViewModel>();
        builder.Services.AddTransient<ItemViewModel>();
        builder.Services.AddSingleton<AuctionListViewModel>();
        builder.Services.AddSingleton<ItemListViewModel>();
        
        // Community Toolkit
        builder.UseMauiApp<App>().UseMauiCommunityToolkit();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        _serviceProvider = builder.Build().Services;
        return builder.Build();
    }
}