using Microsoft.EntityFrameworkCore;
using MVPv4.Client.Pages.Components;
using MVPv4.Data;
using MVPv4.Components;
using MVPv4.Controllers;
using DTOmvp;
using MVPv4.Services;
using MVPv4.Models;
using Microsoft.Extensions.DependencyInjection;
using MVPv4.Client;

namespace MVPv4;

public class Program
{
    // Точка входа в приложение. Вызывается при запуске приложения
    public static void Main(string[] args)
    {
        // Создает новый построитель веб-приложения с предоставленными аргументами командной строки
        // WebApplication.CreateBuilder() создает экземпляр WebApplicationBuilder, который используется для настройки приложения
        var builder = WebApplication.CreateBuilder(args);

        // Настраивает HTTP-клиент с базовым адресом для выполнения API-запросов
        // "MyClient" - это имя клиента, которое будет использоваться для внедрения зависимостей
        // client.BaseAddress устанавливает базовый URL для всех запросов этого клиента
        builder.Services.AddHttpClient("MyClient", client
            => client.BaseAddress = new Uri("https://localhost:7146"
            ?? throw new InvalidOperationException("'applicationUrl' not found.")));

        // Настраивает параметры контекста базы данных для Entity Framework Core
        // options - это объект DbContextOptionsBuilder, который используется для настройки параметров подключения к БД
        var dbContext = (DbContextOptionsBuilder options) =>
        {
            // Настраивает PostgreSQL как провайдер базы данных с строкой подключения из конфигурации
            // UseNpgsql() указывает EF Core использовать PostgreSQL в качестве провайдера БД
            // GetConnectionString() получает строку подключения из файла конфигурации (appsettings.json)
            options.UseNpgsql(
            builder.Configuration.GetConnectionString("MVPv4Context")
            ?? throw new InvalidOperationException("Connection string 'MVPv4Context' not found."));
        };

        // Добавляет MVC-контроллеры в контейнер внедрения зависимостей
        // Это позволяет использовать контроллеры в приложении
        builder.Services.AddControllers();
        //builder.Services.AddOpenApi();

        //builder.Services.AddRazorPages();
        //builder.Services.AddServerSideBlazor();
        //builder.Services.AddEndpointsApiExplorer();

        // Регистрирует фабрику контекста базы данных с настроенными параметрами
        // AddDbContextFactory создает фабрику, которая будет создавать экземпляры контекста БД
        builder.Services.AddDbContextFactory<MVPv4Context>(dbContext);
        //builder.Services.AddScoped<DocumentEditorController>();
        // Регистрирует сервис редактора документов с его интерфейсом
        // AddScoped означает, что новый экземпляр сервиса будет создаваться для каждого HTTP-запроса
        builder.Services.AddScoped<IDocumentEditorService, DocumentEditorService>();
        //builder.Services.AddScoped<TestController>();
        // Регистрирует компонент статуса как сервис-одиночку
        // AddSingleton означает, что будет создан только один экземпляр на все время работы приложения
        builder.Services.AddSingleton<StatusComponent>();
        //builder.Services.AddSwagger();
        // Регистрирует репозиторий персон как сервис-одиночку
        builder.Services.AddSingleton<PersonRepository>();

        // Настраивает Razor Components с поддержкой как серверного, так и WebAssembly рендеринга
        // AddInteractiveServerComponents() добавляет поддержку серверного рендеринга
        // AddInteractiveWebAssemblyComponents() добавляет поддержку клиентского рендеринга через WebAssembly
        builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents()
            .AddInteractiveWebAssemblyComponents();

        // Добавляет адаптер QuickGrid для Entity Framework для функциональности сетки данных
        // Это позволяет использовать компонент QuickGrid с данными из Entity Framework
        builder.Services.AddQuickGridEntityFrameworkAdapter();
        //builder.Services.AddDatabaseDeveloperPageExceptionFilter();

        // Создает веб-приложение на основе настроек
        var app = builder.Build();

        // Настраивает приложение в зависимости от окружения
        if (app.Environment.IsDevelopment())
        {
            // Включает отладку WebAssembly в режиме разработки
            // Это позволяет отлаживать клиентский код, работающий в WebAssembly
            app.UseWebAssemblyDebugging();
            //app.MapOpenApi();
        }
        else
        {
            // Настраивает обработку ошибок и функции безопасности для продакшена
            // UseExceptionHandler настраивает обработчик исключений
            // UseHsts включает HTTP Strict Transport Security
            // UseMigrationsEndPoint настраивает конечную точку для миграций БД
            app.UseExceptionHandler("/Error");
            app.UseHsts();
            app.UseMigrationsEndPoint();
        }

        //app.UseStatusCodePagesWithReExecute("/Error");

        // Сопоставляет маршруты контроллеров
        // Это позволяет обрабатывать HTTP-запросы через контроллеры
        app.MapControllers();

        // Включает перенаправление HTTP на HTTPS для безопасности
        app.UseHttpsRedirection();
        // Сопоставляет статические ресурсы (CSS, JavaScript, изображения)
        app.MapStaticAssets();

        // Настраивает Razor Components с обоими режимами рендеринга
        // AddInteractiveServerRenderMode() включает серверный рендеринг
        // AddInteractiveWebAssemblyRenderMode() включает клиентский рендеринг
        // AddAdditionalAssemblies() добавляет дополнительные сборки для рендеринга
        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode()
            .AddInteractiveWebAssemblyRenderMode()
            .AddAdditionalAssemblies(typeof(Client._Imports).Assembly);

        // Включает проверку токена защиты от подделки
        // Это защищает от CSRF-атак
        app.UseAntiforgery();

        //app.MapControllerRoute(
        //    name: "default",
        //    pattern: "{controller=Home}/{action=Index}/{id?}");

        // Запускает приложение и начинает прослушивать HTTP-запросы
        app.Run();
    }
}
