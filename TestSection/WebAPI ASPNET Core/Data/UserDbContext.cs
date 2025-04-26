using Microsoft.EntityFrameworkCore;
using WebAPI_ASPNET_Core.Models;

namespace WebAPI_ASPNET_Core.Data
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) 
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<UserDocument> UserDocuments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //  Настройка связей
            modelBuilder.Entity<User>()
                .HasMany(u => u.UserDocuments)
                .WithMany(ud => ud.Users)
                .UsingEntity(j => j.ToTable("UserUserDocument"));

            modelBuilder.Entity<UserDocument>()
                .HasOne(ud => ud.Template)
                .WithMany()
                .HasForeignKey(ud => ud.TemplateId);

            base.OnModelCreating(modelBuilder);
        }
    }
}