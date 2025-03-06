using RestSharp;
using Microsoft.Extensions.Configuration;
using DeepSeekDemo.Models;

namespace DeepSeekDemo.Services
{
    public class DeepSeekService
    {
        private readonly RestClient _client;
        private readonly string _apiKey;

        public DeepSeekService(IConfiguration configuration)
        {
            _apiKey = configuration["DEEPSEEK_API_KEY"] ?? "";
            _client = new RestClient(configuration["DEEPSEEK_BASE_URL"] ?? "");
            _client.AddDefaultHeader("Authorization", $"Bearer {_apiKey}");
            _client.AddDefaultHeader("Content-Type", "application/json");
        }

        public async Task<string> SendMessageAsync(string userMessage)
        {
            var request = new RestRequest
            {
                Method = Method.Post
            };

            string systemPrompt = @"You are a debate champion and a master of argumentation.
                As a debate assistant, you are here to challenge the user on complex topics and
                stimulate an intense and thought-provoking conversation. Each time you initiate an interaction,
                you suggest two debate topics and encourage the user to choose one.
                Your attitude is challenging, designed to provoke and stimulate critical thinking, pushing the user to defend their stance rigorously.";

            var chatRequest = new ChatRequest
            {
                Messages = new List<ChatMessage>
                {
                    new ChatMessage { Role = "system", Content = systemPrompt },
                    new ChatMessage { Role = "user", Content = userMessage }
                }
            };

            request.AddJsonBody(chatRequest);

            var response = await _client.ExecuteAsync<ChatResponse>(request);

            if (!response.IsSuccessful)
            {
                throw new Exception($"Error: {response.StatusCode} - {response.ErrorMessage}");
            }

            return response.Data?.Choices?.FirstOrDefault()?.Message?.Content ?? "No response from DeepSeek.";
        }
    }
}