namespace Program.Entities
{
    class Page
    {
        public int Id { get; set; } //Id
        public string Content { get; set; } // Содержание страницы (текст)
        public int PageNumber { get; set; } // Номер страницы
        public int BookId { get; set; } // Внешний ключ, указывающий на книгу
        public Book Book { get; set; } // Навигационное свойство для связи с Book
    }
}
