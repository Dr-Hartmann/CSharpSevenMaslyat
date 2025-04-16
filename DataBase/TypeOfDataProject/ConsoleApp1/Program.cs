



public class User
{
    public Guid ID { get; }
    public string Nickname { get; set; } = "Guest";
    public string Password { get; set; } = string.Empty;
    public State AccessRule = State.Guest;

    //шаблоны TODO
    public List<string/*Структура JSON*/>? Templates = null;
}

public class Template : MainTemplate
{
    public int FontSize { get; set; } = 14;
    public string Font { get; set; } = "Times";
    //public int[4] Paddings { get; } = [3, 2, 1, 2];
    public float LineSpacing { get; set; } = 1f;
    public List<TextBox> BaseSections { get; set; } // Тит, огл, вв, анотация, закл.......
    public List<TextBox> DynamicSections { get; set; } // ?!
    public Align align { get; set; } = Align.Justify;


}

public class Spec : MainTemplate
{

}

public interface MainTemplate
{

}

public class Images
{
    // своё
    // какой ти пданых для картинок ???
}

public class Annex : MainTemplate
{ 
    // нужно ли ?
}


public enum Align // ?!
{
    Left,
    Right,
    Center,
    Justify // ?!
}

public class Table : MainTemplate
{
    ///
}

public struct TextBox
{
    string nameSections;
    string value;
}

public class TextBlock
{

}

public class SuperTextBlock
{

}

public enum State
{
    Admin,
    User,
    Guest
}


// подумать над типами данных специфичных блоков текста






/// <summary>
/// потом
/// </summary>
public class JSONS {

    //
}