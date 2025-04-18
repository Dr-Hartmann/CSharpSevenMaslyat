using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace MVPv4.Client;

class Program
{
    static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);
        
        builder.Services.AddScoped(sp => new HttpClient
        {
            BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
        });

        await builder.Build().RunAsync();
    }
}
