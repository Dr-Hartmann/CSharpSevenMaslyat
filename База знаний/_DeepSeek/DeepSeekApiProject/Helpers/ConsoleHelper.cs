namespace DeepSeekDemo.Helpers;

public static class ConsoleHelper
{
    public static void PrintBotResponse(string response)
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.Write("Bot: ");
        Console.ResetColor();
        Console.WriteLine(response);
    }
}