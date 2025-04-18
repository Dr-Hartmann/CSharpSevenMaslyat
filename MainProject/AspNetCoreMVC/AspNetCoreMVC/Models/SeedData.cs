using AspNetCoreMVC.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace AspNetCoreMVC.Models;

/// <summary>
/// Статический класс для инициализации базы данных начальными данными.
/// Используется при первом запуске приложения или при необходимости сброса данных.
/// </summary>
public static class SeedData
{
    /// <summary>
    /// Метод для инициализации базы данных тестовыми данными.
    /// </summary>
    /// <param name="serviceProvider">
    /// Провайдер сервисов, который используется для получения контекста базы данных.
    /// Позволяет использовать dependency injection для создания экземпляра контекста.
    /// </param>
    public static void Initialize(IServiceProvider serviceProvider)
    {
        // Получение контекста базы данных через dependency injection
        using (var context = new AspNetCoreMVCContext(
            serviceProvider.GetRequiredService<DbContextOptions<AspNetCoreMVCContext>>()))
        {
            // Проверка наличия данных в базе
            if (context.Movies.Any())
            {
                return; // База данных уже содержит данные
            }

            // Добавление начального набора фильмов
            context.Movies.AddRange(
                new Movies
                {
                    Title = "When Harry Met Sally",
                    ReleaseDate = DateTime.Parse("1989-2-12"),
                    Genre = "Romantic Comedy",
                    Price = 7.99M
                },
                new Movies
                {
                    Title = "Ghostbusters ",
                    ReleaseDate = DateTime.Parse("1984-3-13"),
                    Genre = "Comedy",
                    Price = 8.99M
                },
                new Movies
                {
                    Title = "Ghostbusters 2",
                    ReleaseDate = DateTime.Parse("1986-2-23"),
                    Genre = "Comedy",
                    Price = 9.99M
                },
                new Movies
                {
                    Title = "Rio Bravo",
                    ReleaseDate = DateTime.Parse("1959-4-15"),
                    Genre = "Western",
                    Price = 3.99M
                }
            );

            // Сохранение изменений в базе данных
            context.SaveChanges();
        }
    }
}