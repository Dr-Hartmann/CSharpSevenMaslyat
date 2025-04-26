using Microsoft.EntityFrameworkCore;
using MVPv4.Models;

namespace MVPv4.Data;

public class MVPv4Context : DbContext
{
    public MVPv4Context(DbContextOptions<MVPv4Context> options)
        : base(options)
    {
        Database.EnsureCreated();
    }

    public DbSet<DocumentV1> DocumentV1 { get; set; }
}

/* TODO
 * Научиться создавать и объединять миграции
 * Написать интерфейсы и сервисы к ним, внедрить зависимости
 * Создать новые сущности и дополнить контекст (автогенерация)
 */