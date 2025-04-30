using Microsoft.EntityFrameworkCore;
using MVPv5.API.Controllers;
using MVPv5.Application.Services;
using MVPv5.Core.Abstractions;
using MVPv5.Domain.Data;
using MVPv5.Domain.Repositories;

namespace MVPv5.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
                options.JsonSerializerOptions.WriteIndented = true;
            });
        builder.Services.AddOpenApi();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddAntiforgery();

        // TODO - ��� ��������� ��������?
        //builder.Services.AddHttpClient("MyClient", client
        //=> client.BaseAddress = new Uri(builder.Configuration.GetValue<string>("applicationUrl")
        //?? throw new InvalidOperationException("'applicationUrl' not found.")));

        builder.Services.AddDbContextFactory<MVPv5DbContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("MVPv5Database")
            ?? throw new InvalidOperationException("Connection string 'BlazorWebAppMoviesContext' not found.")));

        builder.Services.AddScoped<UserController>();
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<IUserRepository, UserRepository>();

        // TODO - �� ��������
        //builder.Services.AddAuthorization();
        //builder.Services.AddIdentityApiEndpoints<IdentityUser>()
        //    .AddEntityFrameworkStores<MVPv5DbContext>();

        // TODO - �����?
        //builder.Logging.SetMinimumLevel(LogLevel.Debug);

        var app = builder.Build();

        app.UseCors(policy =>
                policy.AllowAnyHeader()
                .AllowAnyMethod()
                .AllowAnyOrigin());

        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/openapi/v1.json", "api");
            });
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
        app.UseStaticFiles();

        //app.MapControllerRoute(
        //    name: "default",
        //    pattern: "{controller=Home}/{action=Index}/{id?}");

        app.UseAntiforgery();
        app.Run();
    }
}
