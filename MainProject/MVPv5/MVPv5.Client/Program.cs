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

        // можно ли отвязать клиента от жёсткой привязки к URI?
        builder.Services.AddScoped(sp => new HttpClient()
        {
            BaseAddress = new Uri(builder.Configuration["WebApiAddress"]!)
        });

        await builder.Build().RunAsync();
    }
}
