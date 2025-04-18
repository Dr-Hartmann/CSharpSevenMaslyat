using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using AspNetCoreMVC.Data;
using AspNetCoreMVC.Models;
namespace AspNetCoreMVC;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddDbContext<AspNetCoreMVCContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("AspNetCoreMVCContext") ?? throw new InvalidOperationException("Connection string 'AspNetCoreMVCContext' not found.")));

        builder.Services.AddControllersWithViews();
        //builder.Services.AddScoped<IApplicationBuilder, ApplicationBuilder>();

        var app = builder.Build();

        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            SeedData.Initialize(services);
        }

        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}
