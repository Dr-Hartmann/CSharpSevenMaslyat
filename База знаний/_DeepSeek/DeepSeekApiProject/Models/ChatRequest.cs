namespace DeepSeekDemo.Models
{
    public class ChatRequest
    {
        public string Model { get; set; } = "deepseek-chat";
        public List<ChatMessage> Messages { get; set; }
        public bool Stream { get; set; } = false;
    }

    public class ChatMessage
    {
        public string Role { get; set; } = "";
        public string Content { get; set; } = "";
    }
}