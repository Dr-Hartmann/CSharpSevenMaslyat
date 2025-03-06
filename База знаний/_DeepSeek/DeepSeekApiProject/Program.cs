using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using DeepSeek.Core;
using DeepSeek.Core.Models;

using DeepSeekDemo.Helpers;
using DeepSeekDemo.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

class Program
{
    private static readonly HttpClient client = new HttpClient();

    static async Task Main(string[] args)
    {
        // // Замените на ваш API ключ и URL
        // string apiKey = "sk-50096063ce914d45904609b55bd26626";
        // string apiUrl = "https://api.deepseek.com"; // Замените на реальный URL API

        // // Пример JSON тела запроса (замените на реальные данные)
        // var json = "{\"key\": \"value\"}";
        // var content = new StringContent(json, Encoding.UTF8, "application/json");

        // // Добавление заголовков (если требуется)
        // client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

        // // Отправка POST запроса
        // HttpResponseMessage response = await client.PostAsync(apiUrl, content);

        // if (response.IsSuccessStatusCode)
        // {
        //     string responseBody = await response.Content.ReadAsStringAsync();
        //     Console.WriteLine("Ответ от API:");
        //     Console.WriteLine(responseBody);
        // }
        // else
        // {
        //     Console.WriteLine($"Ошибка: {response.StatusCode}");
        // }



        // var client = new DeepSeekClient("sk-50096063ce914d45904609b55bd26626");
        // var modelResponse = await client.ListModelsAsync(new CancellationToken());
        // if (modelResponse is null)
        // {
        //     Console.WriteLine(client.ErrorMsg);
        //     return;
        // }
        // foreach (var model in modelResponse.Data)
        // {
        //     Console.WriteLine(model);
        // }




    }
}