namespace DeepSeekDemo.Models
{
    public class ChatResponse
    {
        public string Id { get; set; } = "";
        public string Object { get; set; } = "";
        public long Created { get; set; }
        public string Model { get; set; } = "";
        public List<ChatChoice> Choices { get; set; }
    }

    public class ChatChoice
    {
        public ChatMessage Message { get; set; }
        public int Index { get; set; }
        public string FinishReason { get; set; } = "";
    }
}