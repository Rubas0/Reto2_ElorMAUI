using Microsoft.Extensions.Logging;
using Reto02_MAUI.Services;
using Reto02_MAUI.Shared.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Reto02_MAUI
{
    public static class MauiProgram
    {
        public static App CreateMauiApp()
        {
            var builder = App.CreateBuilder();
            builder
                .UseMauiApp<App>()

                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            // Add device-specific services used by the Reto02_MAUI.Shared project
            builder.Services.AddSingleton<IFormFactor, FormFactor>();
            builder.Services.AddSingleton<ICentroService, CentroService>();
            builder.Services.AddSingleton<IWeatherService, WeatherService>();

            builder.Services.AddHttpClient();

            builder.Services.AddMauiBlazorWebView();

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}