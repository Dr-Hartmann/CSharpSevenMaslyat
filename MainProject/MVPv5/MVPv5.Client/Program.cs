using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace MVPv5.Client;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);
        builder.RootComponents.Add<App>("#app");
        builder.RootComponents.Add<HeadOutlet>("head::after");

        builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

        //builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.Configuration["WebApiAddress"]!) });

        //builder.Services.AddCascadingAuthenticationState();
        //builder.Services.AddAuthorizationCore();
        //builder.Services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();
        //builder.Services.AddBlazoredLocalStorage();

        await builder.Build().RunAsync();
    }
}
