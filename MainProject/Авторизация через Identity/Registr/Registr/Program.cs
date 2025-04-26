
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Registr.Services;

namespace Registr
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddOpenApi();
            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer("name=DefConn");
            });

            builder.Services.AddAuthorization();
            builder.Services.AddIdentityApiEndpoints<IdentityUser>()
                .AddEntityFrameworkStores<AppDbContext>();

            var app = builder.Build();

            app.UseCors(policy => 
                policy.AllowAnyHeader()
                .AllowAnyMethod()
                .AllowAnyOrigin());

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/openapi/v1.json", "api");
                });
            }

            app.MapIdentityApi<IdentityUser>();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
