using Microsoft.Extensions.Logging;
using Reto02_MAUI.Services;

namespace Reto02_MAUI
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
                    fonts.AddFont("OpenSans-Regular. ttf", "OpenSansRegular");
                });

            // Registrar servicios Blazor
            builder.Services.AddMauiBlazorWebView();

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif

            // Registrar servicios personalizados
            builder.Services.AddSingleton<ICentroService, CentroService>();
            builder.Services.AddSingleton<IWeatherService, WeatherService>();

            return builder.Build();
        }
    }
}