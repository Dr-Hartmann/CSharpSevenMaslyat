using Microsoft.EntityFrameworkCore;
using Program.Entities;

namespace Program
{
    class Context : DbContext
    {
        public Context()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(@"host=localhost;port=5532;database=7maslyat;username=postgres;password=553210zxc");
        }

        public DbSet<Book> Book {get; set; }
    }

    //public class BookContext : DbContext
    //{
    //    public DbSet<Book> Books { get; set; }
    //    public DbSet<Page> Pages { get; set; }

    //    public BookContext(DbContextOptions<BookContext> options) : base(options)
    //    {
    //    }

    //    protected override void OnModelCreating(ModelBuilder modelBuilder)
    //    {
    //        // Явное указание связи (необязательно, но рекомендуется для контроля)
    //        modelBuilder.Entity<Book>()
    //            .HasMany(b => b.Pages)
    //            .WithOne(p => p.Book)
    //            .HasForeignKey(p => p.BookId); // BookId – внешний ключ в таблице Pages

    //        base.OnModelCreating(modelBuilder);
    //    }
    //}
}
