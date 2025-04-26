using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using WebAPI_ASPNET_Core.Data;
using WebAPI_ASPNET_Core.Services;

var builder = WebApplication.CreateBuilder(args);

// Добаваление сервисов в контейнер
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Конфигурация строк подключения
var configuration = builder.Configuration;

// Регистрация контекстов БД
builder.Services.AddDbContext<UserDbContext>(
    options =>
    {
        options.UseNpgsql(configuration.GetConnectionString("UserDb"));
    });

builder.Services.AddDbContext<TemplateDbContext>(
    options =>
    {
        options.UseNpgsql(configuration.GetConnectionString("TemplateDb"));
    });

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
