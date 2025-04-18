
namespace TestDeepSeek;

internal class Program
{
    static async Task Main(string[] args)
    {
        DeepSeekService model = new();
        string a = await model.GetResponseAsync("2+2=?");
        Console.WriteLine(a);
    }
}
