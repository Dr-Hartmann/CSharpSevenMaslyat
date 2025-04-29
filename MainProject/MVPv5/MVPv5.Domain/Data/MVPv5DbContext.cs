using Microsoft.EntityFrameworkCore;
using MVPv5.Domain.Entities;

namespace MVPv5.Domain.Data;

public class MVPv5DbContext : DbContext
{
    public MVPv5DbContext(DbContextOptions<MVPv5DbContext> options)
        :base(options)
    {
        Database.EnsureCreated();
    }
    public DbSet<UserEntity> TableUsers { get; set; }
    public DbSet<DocumentEntity> TableDocuments { get; set; }
}

/* TODO
 * Научиться создавать и объединять миграции
 * Написать интерфейсы и сервисы к ним, внедрить зависимости
 * Создать новые сущности и дополнить контекст
 */