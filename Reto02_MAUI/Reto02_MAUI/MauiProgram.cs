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
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            // Registrar Blazor WebView
            builder.Services.AddMauiBlazorWebView();

#if DEBUG
            // Herramientas de desarrollo solo en modo Debug
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif

            //REGISTRO DE SERVICIOS PERSONALIZADOS 

            // Servicio de centros educativos JSON 
            builder.Services.AddSingleton<ICentroService, CentroService>();

            // Servicio OpenWeatherMap
            builder.Services.AddSingleton<IWeatherService, WeatherService>();

            // HttpClient
            builder.Services.AddSingleton<HttpClient>();

            Console.WriteLine("ICentroService -> CentroService (JSON local)");
            Console.WriteLine("IWeatherService -> WeatherService (OpenWeatherMap API)");

            return builder.Build();
        }
    }
}