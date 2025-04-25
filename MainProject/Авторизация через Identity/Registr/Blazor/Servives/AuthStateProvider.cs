using Blazored.LocalStorage;
using DTO;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text.Json.Nodes;

namespace Blazor.Servives
{
    public class AuthStateProvider : AuthenticationStateProvider
    {
        private readonly HttpClient httpClient;
        private readonly ISyncLocalStorageService localsorage;

        public AuthStateProvider(HttpClient httpClient, ISyncLocalStorageService localsorage)
        {
            this.httpClient = httpClient;
            this.localsorage = localsorage;

            var accessToken = localsorage.GetItem<string>("accessToken");
            if (accessToken != null)
            {
                this.httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            }
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            // var user = new ClaimsPrincipal(new ClaimsIdentity()); // non-auth user

            //var claims = new List<Claim> { new Claim(ClaimTypes.Name, "Jonh") };
            //var identity = new ClaimsIdentity(claims, "ANY");
            //var user = new ClaimsPrincipal(identity);

            //return Task.FromResult(new AuthenticationState(user));

            var user = new ClaimsPrincipal(new ClaimsIdentity()); // non-auth user

            try
            {
                var response = await httpClient.GetAsync("manage/info");
                if (response.IsSuccessStatusCode)
                {
                    var strResponse = await response.Content.ReadAsStringAsync();
                    var jsonResponse = JsonNode.Parse(strResponse);
                    var email = jsonResponse!["email"]!.ToString();

                    var claims = new List<Claim>
                    {
                        new(ClaimTypes.Name, email),
                        new(ClaimTypes.Email, email),
                    };

                    var indentity = new ClaimsIdentity(claims, "Token");
                    user = new ClaimsPrincipal(indentity);
                    return new AuthenticationState(user);
                }
            }
            catch(Exception ex)
            {
            }

            return new AuthenticationState(user);

        }

        public async Task<FromResult> LoginAsync(string email, string password)
        {
            try
            {
                var response = await httpClient.PostAsJsonAsync("login", new { email, password });
                if (response.IsSuccessStatusCode)
                {
                    var strResponse = await response.Content.ReadAsStringAsync();
                    var jsonResponse = JsonNode.Parse(strResponse);
                    var accessToken = jsonResponse?["accessToken"]?.ToString();
                    var refreshToken = jsonResponse?["refreshToken"]?.ToString();

                    localsorage.SetItem("accessToken", accessToken);
                    localsorage.SetItem("refreshToken", accessToken);

                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                    // need to refresh auth state
                    NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());

                    //success
                    return new FromResult { Succeeded = true };
                }
                else
                {
                    return new FromResult { Succeeded = false, Errors = ["Bad Email or Password"] };
                }
            }
            catch { }

            return new FromResult { Succeeded = false, Errors = ["Connection Error"] };
        }

        public void Logout()
        {
            localsorage.RemoveItem("accessToken");
            localsorage.RemoveItem("refreshToken");
            httpClient.DefaultRequestHeaders.Authorization = null;
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public async Task<FromResult> RegisterAsync(RegisterDTO registerDTO)
        {
            try
            {
                var response = await httpClient.PostAsJsonAsync("register", registerDTO);
                if (response.IsSuccessStatusCode)
                {
                    // return new FromResult { Succeeded = true };
                    var loginResponse = await LoginAsync(registerDTO.Email, registerDTO.Password);
                    return loginResponse;
                }

                // Errors
                var strResponse = await response.Content.ReadAsStringAsync();
                Console.WriteLine(strResponse);
                var jsonResponse = JsonNode.Parse(strResponse);
                var errorsObj = jsonResponse!["errors"]!.AsObject();
                var errorsList = new List<string>();
                foreach (var error in errorsObj)
                {
                    errorsList.Add(error.Value![0]!.ToString());
                }

                var formResult = new FromResult { Succeeded = false, Errors = errorsList.ToArray() };
                return formResult;

            }
            catch { }

            return new FromResult { Succeeded = false, Errors = ["Connection Error"] };
        }
    }

}
