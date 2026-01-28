using Reto2FINAL.Services;
using Microsoft.Extensions.Logging;

namespace Reto2FINAL
{
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

            builder.Services.AddMauiBlazorWebView();

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif

            // Registrar HttpClient
            builder.Services.AddSingleton<HttpClient>();

            // Registrar servicios
            builder.Services.AddSingleton<CentrosService>();
            builder.Services.AddSingleton<WeatherService>();

            return builder.Build();
        }
    }
}