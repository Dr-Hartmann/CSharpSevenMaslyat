using Microsoft.EntityFrameworkCore;
using MVPv5.API.Entities;

namespace MVPv5.API.Data;

public class MVPv5DbContext : DbContext
{
    public MVPv5DbContext(DbContextOptions<MVPv5DbContext> options)
        :base(options)
    {
        Database.EnsureCreated();
    }
    public DbSet<UserEntity> Users { get; set; }
    //public DbSet<UserEntity> Users { get; set; }
    //public DbSet<UserEntity> Users { get; set; }
}

/* TODO
 * Научиться создавать и объединять миграции
 * Написать интерфейсы и сервисы к ним, внедрить зависимости
 * Создать новые сущности и дополнить контекст
 */