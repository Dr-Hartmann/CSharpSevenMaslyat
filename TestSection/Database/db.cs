public class Book
{
    public int Id { get; set; } //Id
    public string Font { get; set; } //Тип шрифта
    public int Size { get; set; } //Размер шрифта
    public string Align { get; set; } //Выравнивание
    public int Interv { get; set; } //Межстрочный интервал
    public int Indent { get; set; } //Отступ
    public string Outline { get; set; } //Начертание (Жирный/курсив)
    public bool bef_parag { get; set; } //Наличие отступа перед абзацем
    public bool aft_parag { get; set; } //Наличие отступа после абзаца
}

public class Page
{
    public int Id { get; set; } //Id
    public string Content { get; set; } // Содержание страницы (текст)
    public int PageNumber { get; set; } // Номер страницы
    public int BookId { get; set; } // Внешний ключ, указывающий на книгу
    public Book Book { get; set; } // Навигационное свойство для связи с Book
}

public class BookContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Page> Pages { get; set; }

        public BookContext(DbContextOptions<BookContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Явное указание связи (необязательно, но рекомендуется для контроля)
            modelBuilder.Entity<Book>()
                .HasMany(b => b.Pages)
                .WithOne(p => p.Book)
                .HasForeignKey(p => p.BookId); // BookId – внешний ключ в таблице Pages

            base.OnModelCreating(modelBuilder);
        }
    }