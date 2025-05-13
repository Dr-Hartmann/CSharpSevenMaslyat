using Microsoft.EntityFrameworkCore;
using MVPv5.API.Controllers.v1;
using MVPv5.Application.Services.v1;
using MVPv5.Core.Abstractions.v1;
using MVPv5.Domain.Data;
using MVPv5.Domain.Repositories;


namespace MVPv5.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
            //.AddJsonOptions(options =>
            //{
            //    options.JsonSerializerOptions.PropertyNamingPolicy = null;
            //    options.JsonSerializerOptions.WriteIndented = true;
            //});
        builder.Services.AddOpenApi();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddAntiforgery();

        builder.Services.AddDbContextFactory<MVPv5DbContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString(nameof(MVPv5DbContext))
            ?? throw new InvalidOperationException("Connection string 'BlazorWebAppMoviesContext' not found.")));

        builder.Services.AddScoped<UserController>();
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<IUserRepository, UserRepository>();

        builder.Services.AddScoped<TemplateController>();
        builder.Services.AddScoped<ITemplateService, TemplateService>();
        builder.Services.AddScoped<ITemplateRepository, TemplateRepository>();

        //builder.Services.AddAuthorization();
        //builder.Services.AddIdentityApiEndpoints<IdentityUser>()
        //    .AddEntityFrameworkStores<MVPv5DbContext>();

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
