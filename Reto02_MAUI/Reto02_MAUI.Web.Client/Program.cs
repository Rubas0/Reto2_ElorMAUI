using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Reto02_MAUI.Shared.Services;
using Reto02_MAUI.Web.Client.Services;

namespace Reto02_MAUI.Web.Client
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            // Add device-specific services used by the Reto02_MAUI.Shared project
            builder.Services.AddSingleton<IFormFactor, FormFactor>();

            await builder.Build().RunAsync();
        }
    }
}
