using Microsoft.EntityFrameworkCore;
using MVPv5.API.Controllers.v1;
using MVPv5.API.Middleware;
using MVPv5.Domain.Abstractions.v1;
using MVPv5.Domain.Data;
using MVPv5.Domain.Repositories;
using MVPv5.Domain.Services.v1;

namespace MVPv5.API;

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

        // TODO - точно работает проверка?
        builder.Services.AddDbContextFactory<MVPv5DbContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString(nameof(MVPv5DbContext))
            ?? throw new InvalidOperationException($"'{nameof(MVPv5DbContext)}' not found.")));

        //builder.Services.AddScoped<UserController>();
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<IUserRepository, UserRepository>();

        //builder.Services.AddScoped<TemplateController>();
        builder.Services.AddScoped<ITemplateService, TemplateService>();
        builder.Services.AddScoped<ITemplateRepository, TemplateRepository>();

        //builder.Services.AddScoped<DocumentController>();
        builder.Services.AddScoped<IDocumentService, DocumentService>();
        builder.Services.AddScoped<IDocumentRepository, DocumentRepository>();

        var app = builder.Build();

        // TODO - как настроить корсы для билда?
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
                options.IndexStream = ()
                    => File.OpenRead(Path.Combine(app.Environment.WebRootPath!, "swagger", "swagger-index.html"));
            });
            app.UseMigrationsEndPoint();
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();        
        app.UseMiddleware<ExMiddleware>();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();
        app.UseAntiforgery();
        app.Run();
    }
}
