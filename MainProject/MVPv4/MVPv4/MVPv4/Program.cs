using Microsoft.EntityFrameworkCore;
using MVPv4.Data;
using MVPv4.Components;
using MVPv4.Controllers;
using DTOmvp;
using MVPv4.Services;
using MVPv4.Models;
using Microsoft.Extensions.DependencyInjection;

namespace MVPv4;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddHttpClient("MyClient", client
            => client.BaseAddress = new Uri(builder.Configuration.GetValue<string>("applicationUrl")
            ?? throw new InvalidOperationException("'applicationUrl' not found.")));

        builder.Services.AddControllers();
        builder.Services.AddDbContextFactory<MVPv4Context>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("MVPv4Context")
            ?? throw new InvalidOperationException("Connection string 'MVPv4Context' not found.")));

        builder.Services.AddScoped<IDocumentEditorService, DocumentEditorService>();
        //builder.Services.AddSingleton<StatusComponent>();
        builder.Services.AddQuickGridEntityFrameworkAdapter();

        builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents()
            .AddInteractiveWebAssemblyComponents();

        //builder.Services.AddOpenApi();
        //builder.Services.AddRazorPages();
        //builder.Services.AddServerSideBlazor();
        //builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddScoped<DocumentEditorController>();

        //builder.Services.AddScoped<TestController>();

        //builder.Services.AddDatabaseDeveloperPageExceptionFilter();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseWebAssemblyDebugging();
            //app.MapOpenApi();
        }
        else
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
            app.UseMigrationsEndPoint();
        }

        //app.UseStatusCodePagesWithReExecute("/Error");

        app.MapControllers();

        app.UseHttpsRedirection();
        app.MapStaticAssets();

        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode()
            .AddInteractiveWebAssemblyRenderMode()
            .AddAdditionalAssemblies(typeof(Client._Imports).Assembly);

        app.UseAntiforgery();

        //app.MapControllerRoute(
        //    name: "default",
        //    pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}
