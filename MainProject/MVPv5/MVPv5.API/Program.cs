using Microsoft.EntityFrameworkCore;
using MVPv5.API.Controllers.v1;
using MVPv5.Application.Services.v1;
using MVPv5.Core.Abstractions.v1;
using MVPv5.Domain.Data;
using MVPv5.Domain.Repositories;

namespace MVPv5.API;

// TODO - порядок не менять!

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddAntiforgery();
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddOpenApi();
        builder.Services.AddAuthentication();
        builder.Services.AddAuthorization();

        builder.Services.AddDbContextFactory<MVPv5DbContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString(nameof(MVPv5DbContext))
            ?? throw new InvalidOperationException("Connection string 'BlazorWebAppMoviesContext' not found.")));

        builder.Services.AddScoped<UserController>();
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<IUserRepository, UserRepository>();

        builder.Services.AddScoped<TemplateController>();
        builder.Services.AddScoped<ITemplateService, TemplateService>();
        builder.Services.AddScoped<ITemplateRepository, TemplateRepository>();        

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
                options.SwaggerEndpoint("/openapi/v1.json", "v1");
            });
            app.UseMigrationsEndPoint();
        }
        //else
        //{
        //    app.UseExceptionHandler("/Error");
        //    app.UseHsts();
        //}

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        //app.UseRouting();
        app.UseCors();
        app.UseAuthentication();
        app.UseAuthorization();

        // свой Middleware<>()

        app.MapControllers();
        app.UseAntiforgery();
        app.Run();
    }
}
