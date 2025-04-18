using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using AspNetCoreMVC.Data;
using AspNetCoreMVC.Models;
namespace AspNetCoreMVC;

/// <summary>
/// Класс Program содержит точку входа в приложение.
/// В ASP.NET Core 6+ используется минимальный API, где Program.cs заменяет Startup.cs.
/// </summary>
public class Program
{
    /// <summary>
    /// Точка входа в приложение.
    /// </summary>
    /// <param name="args">Аргументы командной строки.</param>
    public static void Main(string[] args)
    {
        // Создаем экземпляр WebApplicationBuilder для настройки приложения
        var builder = WebApplication.CreateBuilder(args);

        // Регистрируем контекст базы данных в DI-контейнере
        // UseNpgsql - указывает, что используется PostgreSQL
        // GetConnectionString - получает строку подключения из appsettings.json
        builder.Services.AddDbContext<AspNetCoreMVCContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("AspNetCoreMVCContext") ?? throw new InvalidOperationException("Connection string 'AspNetCoreMVCContext' not found.")));

        // Регистрируем сервисы для MVC
        builder.Services.AddControllersWithViews();
        //builder.Services.AddScoped<IApplicationBuilder, ApplicationBuilder>();

        // Создаем экземпляр приложения
        var app = builder.Build();

        // Создаем область видимости для сервисов
        using (var scope = app.Services.CreateScope())
        {
            // Получаем провайдер сервисов
            var services = scope.ServiceProvider;
            // Инициализируем начальные данные в базе
            SeedData.Initialize(services);
        }

        // Настройка обработки ошибок для продакшн-окружения
        if (!app.Environment.IsDevelopment())
        {
            // Используем специальный обработчик ошибок
            app.UseExceptionHandler("/Home/Error");
            // Включаем HSTS (HTTP Strict Transport Security)
            app.UseHsts();
        }

        // Включаем перенаправление с HTTP на HTTPS
        app.UseHttpsRedirection();
        // Включаем обслуживание статических файлов (CSS, JavaScript, изображения)
        app.UseStaticFiles();

        // Включаем маршрутизацию
        app.UseRouting();

        // Включаем авторизацию
        app.UseAuthorization();

        // Настраиваем маршруты по умолчанию
        // {controller=Home} - контроллер по умолчанию
        // {action=Index} - действие по умолчанию
        // {id?} - необязательный параметр id
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        // Запускаем приложение
        app.Run();
    }
}
