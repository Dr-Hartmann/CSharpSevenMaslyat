namespace MVPv5.Core.Models;

public class TemplateModel
{
    private TemplateModel(int id, string name, string type, DateOnly dateCreation)
    {
        Id = id;
        Name = name;
        Type = type;
        DateCreation = dateCreation;
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
    public DateOnly DateCreation { get; set; }
    //public string? FilePath { get; set; }// Если храним файл на диске

    public static (TemplateModel, string) Create(int id, string name, string type, DateOnly dateCreation)
    {
        var error = string.Empty;

        //if (string.IsNullOrEmpty(nickname) || string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
        //{
        //    error = "Никнейм, логин или пароль пусты";
        //}

        return (new TemplateModel(id, name, type, dateCreation), error);
    }
}
