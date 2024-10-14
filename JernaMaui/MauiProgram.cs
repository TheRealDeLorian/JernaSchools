using JernaClassLib;
using JernaClassLib.IServices;
using JernaMaui.Services;
using Microsoft.Extensions.Logging;

namespace JernaMaui;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            });

        // builder.Services.AddScoped(o => new HttpClient() { BaseAddress = new Uri(builder.Configuration[Constants.MuaiBaseAddress] ?? "https://localhost:7187/api/") });
        builder.Services.AddScoped(o => new HttpClient() { BaseAddress = new Uri("https://localhost:7187/api/") });
        builder.Services.AddScoped<ISecureStorageService, MauiSecureStorageService>();
        builder.Services.AddScoped<IEmailService, MauiMailService>();
        builder.Services.AddScoped<IItemService, MauiItemService>();
        builder.Services.AddScoped<IAuthService, MauiAuthService>();
        builder.Services.AddScoped<ICartService, MauiCartService>();
        builder.Services.AddScoped<IPurchaseService, MauiPurchaseService>();
        builder.Services.AddScoped<ITagService, MauiTagService>();
        builder.Services.AddSingleton(SecureStorage.Default);
        builder.Services.AddScoped<JernaAuthState>();
        builder.Services.AddScoped<EventService>();
        builder.Services.AddMauiBlazorWebView();
        builder.Configuration[Constants.ApplicationType] = Constants.MauiApplication;
        builder.Services.AddCascadingValue("JernaAuthState", sp =>
        {
            return sp.GetRequiredService<JernaAuthState>();
        });

#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
