using DTOmvp;
using MVPv4.Data;
using MVPv4.Services;
using WebApp.Services;

namespace WebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });

            builder.Services.AddAuthentication("BasicAuthentication")
                .AddScheme<Microsoft.AspNetCore.Authentication.AuthenticationSchemeOptions, WebApp.Services.BasicAuthenticationHandler>("BasicAuthentication", null);

            if (builder.Environment.IsDevelopment())
            {
                builder.Services.AddSingleton<IDocumentEditorService, MockDocumentEditorService>();
            }
            else
            {
                builder.Services.AddScoped<IDocumentEditorService, DocumentEditorService>();
            }

            builder.Services.AddSingleton<AuditService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseCors();
            app.UseAuthentication();

            // app.UseRouting();

            app.MapControllers();

            app.Run();
        }
    }
}
