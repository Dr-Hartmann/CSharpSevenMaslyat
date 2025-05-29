using Microsoft.EntityFrameworkCore;
using MVPv5.Domain.Entities;

namespace MVPv5.Domain.Data;

public class MVPv5DbContext : DbContext
{
    public MVPv5DbContext(DbContextOptions<MVPv5DbContext> options)
        : base(options)
    {
        Database.EnsureCreated();
    }
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<DocumentEntity> Documents { get; set; }
    public DbSet<TemplateEntity> Templates { get; set; }
}

/* TODO
 * Научиться объединять миграции
 */