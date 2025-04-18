using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AspNetCoreMVC.Models;

namespace AspNetCoreMVC.Data
{
    /// <summary>
    /// Контекст базы данных для приложения.
    /// Наследуется от DbContext и предоставляет доступ к таблицам базы данных.
    /// </summary>
    public class AspNetCoreMVCContext : DbContext
    {
        /// <summary>
        /// Конструктор контекста базы данных.
        /// </summary>
        /// <param name="options">
        /// Опции для настройки контекста базы данных.
        /// Включает строку подключения, провайдер базы данных и другие настройки.
        /// </param>
        public AspNetCoreMVCContext (DbContextOptions<AspNetCoreMVCContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Свойство для доступа к таблице Movies в базе данных.
        /// DbSet<T> представляет собой коллекцию сущностей определенного типа.
        /// Используется для выполнения операций CRUD с таблицей Movies.
        /// </summary>
        public DbSet<AspNetCoreMVC.Models.Movies> Movies { get; set; } = default!;
    }
}
