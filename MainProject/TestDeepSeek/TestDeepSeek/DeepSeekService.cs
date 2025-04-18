using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace TestDeepSeek
{
    public class DeepSeekService
    {
        private readonly HttpClient _httpClient;
        private const string ApiKey = "sk-3b68607633d040b38b754e80f284a364"; // если требуется
        private const string ApiUrl = "https://api.deepseek.com/v1/chat/completions"; // пример (уточните URL)

        public DeepSeekService()
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {ApiKey}");
        }

        public async Task<string> GetResponseAsync(string prompt)
        {
            var requestData = new
            {
                model = "deepseek-chat",
                messages = new[] { new { role = "user", content = prompt } }
            };

            var response = await _httpClient.PostAsJsonAsync(ApiUrl, requestData);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
            return result;
        }
    }
}
