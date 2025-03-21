namespace Program.Entities
{
    class Book
    {
        public int Id { get; set; } //Id
        public string Title { get; set; } //Заголовок
        public string Font { get; set; } //Тип шрифта
        public int Size { get; set; } //Размер шрифта
        public string Align { get; set; } //Выравнивание
        public double Interv { get; set; } //Межстрочный интервал
        public double Indent { get; set; } //Отступ
        public string Outline { get; set; } //Начертание (Жирный/курсив)
        public bool Bef_parag { get; set; } //Наличие отступа перед абзацем
        public bool Aft_parag { get; set; } //Наличие отступа после абзаца
        public List<Page> Pages { get; set; } = new List<Page>(); //Количество страниц
    }
}
