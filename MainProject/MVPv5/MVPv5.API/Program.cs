using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MVPv5.API.Controllers;
using MVPv5.API.Data;
using MVPv5.API.Services;

namespace MVPv5.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // TODO - как создавать клиентов?
        //builder.Services.AddHttpClient("MyClient", client
        //=> client.BaseAddress = new Uri(builder.Configuration.GetValue<string>("applicationUrl")
        //?? throw new InvalidOperationException("'applicationUrl' not found.")));

        builder.Services.AddDbContextFactory<MVPv5DbContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("MVPv5Database")
            ?? throw new InvalidOperationException("Connection string 'BlazorWebAppMoviesContext' not found.")));

        builder.Services.AddScoped<UserController>();
        builder.Services.AddTransient<UserService>(); // AddScoped
        //builder.Services.AddSingleton<StatusComponent>();

        builder.Services.AddControllers();
        builder.Services.AddOpenApi();
        builder.Services.AddEndpointsApiExplorer();

        // TODO - не работает
        //builder.Services.AddAuthorization();
        //builder.Services.AddIdentityApiEndpoints<IdentityUser>()
        //    .AddEntityFrameworkStores<MVPv5DbContext>();

        // TODO - нужен?
        //builder.Logging.SetMinimumLevel(LogLevel.Debug);

        var app = builder.Build();

        app.UseCors(policy =>
                policy.AllowAnyHeader()
                .AllowAnyMethod()
                .AllowAnyOrigin());

        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            //app.UseSwaggerUI(options =>
            //{
            //    options.SwaggerEndpoint("/openapi/v1.json", "api");
            //});

            //или

            //app.UseSwagger();
            //app.UseSwaggerUI(options =>
            //{
            //    options.SwaggerEndpoint("/swagger/v1/swagger.json", "API");
            //    options.RoutePrefix = string.Empty;
            //});

        }
        //else
        //{
        //    app.UseExceptionHandler("/Error");
        //    app.UseMigrationsEndPoint();
        //}

        app.UseCors();

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
        //app.UseAntiforgery();
        app.UseStaticFiles();

        //app.MapControllerRoute(
        //    name: "default",
        //    pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}
