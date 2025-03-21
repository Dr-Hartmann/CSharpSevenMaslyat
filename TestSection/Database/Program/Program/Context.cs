using Microsoft.EntityFrameworkCore;

namespace Program
{
    class Context : DbContext
    {
        public Context()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(@"host=localhost;port=5532;database=db;username=root;pass=5532");
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
