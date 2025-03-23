using System.Threading.Tasks;
using RestSharp;

namespace BETSOS
{
    public static class RestApiInterface
    {
        private static readonly RestClient client = new RestClient();

        public static async Task<Struct1DTO> GetAsync(string url)
        {
            var request = new RestRequest(url, Method.Get);
            var response = await client.ExecuteAsync<Struct1DTO>(request);
            return response.Data;
        }

        public static async Task PostAsync(string url, Struct1DTO dto)
        {
            var request = new RestRequest(url, Method.Post);
            request.AddJsonBody((object)dto);
            await client.ExecuteAsync(request);
        }

        public static async Task PutAsync(string url, Struct1DTO dto)
        {
            var request = new RestRequest(url, Method.Put);
            request.AddJsonBody((object)dto);
            await client.ExecuteAsync(request);
        }

        public static async Task PatchAsync(string url, Struct1DTO dto)
        {
            var request = new RestRequest(url, Method.Patch);
            request.AddJsonBody((object)dto);
            await client.ExecuteAsync(request);
        }

        public static async Task DeleteAsync(string url)
        {
            var request = new RestRequest(url, Method.Delete);
            await client.ExecuteAsync(request);
        }
    }
}