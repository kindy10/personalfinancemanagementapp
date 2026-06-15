using PersonalFinance.Mobile.Helpers;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using static SkiaSharp.HarfBuzz.SKShaper;


namespace PersonalFinance.Mobile.Services
{
    public  class ApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService()
        {
            _httpClient = new HttpClient()
            {
                BaseAddress = new Uri(ApiSettings.BaseUrl)
            };

        }

        private async Task AddAuthHeader()
        {
            var token = await SecureStorage.GetAsync("auth_token");

            if (!string.IsNullOrEmpty(token)) {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
        }
        //-----------------------Generic Get
        public async Task<T> GetAsync<T>(string endpoint)
        {
            await AddAuthHeader();

            var response = await _httpClient.GetAsync(endpoint);

            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                try
                {
                    using var document = JsonDocument.Parse(content);

                    var message =
                        document.RootElement
                            .GetProperty("message")
                            .GetString();

                    throw new Exception(message);
                }
                catch (JsonException)
                {
                    throw new Exception(
                        "An unexpected error occurred When parsing the JSON.");
                }
            }

            return JsonSerializer.Deserialize<T>(content,new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        //-----------------------------------Create post
        public async Task<T> PostAsync<T>(string endpoint, object data)
        {
            var json = JsonSerializer.Serialize(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            //Get saved JWT token
            var token = await SecureStorage.GetAsync("auth_token");

            //Attach JWT token
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            //Send Post request 
            var response = await  _httpClient.PostAsync(endpoint, content);

            
            // Read response body
            var result =
                await response.Content.ReadAsStringAsync();

            // Throw detailed error if failed
            if (!response.IsSuccessStatusCode)
            {
                try
                {
                    using var document = JsonDocument.Parse(result);

                    var message =
                        document.RootElement
                            .GetProperty("message")
                            .GetString();

                    throw new Exception(message);
                }
                catch (JsonException)
                {
                    throw new Exception(
                        "An unexpected error occurred When parsing the JSON.");
                }
            }

            // Deserialize JSON
            return JsonSerializer.Deserialize<T>(result, new JsonSerializerOptions{ PropertyNameCaseInsensitive = true});

        }

        //---------------------------------------------Put
        public async Task PutAsync(string endpoint,object data)
        {
            //Convert object  to json
            await AddAuthHeader();

            var json = JsonSerializer.Serialize(data);

            var content = new StringContent(json,Encoding.UTF8, "application/json");

            //Add JWT token

            var token = await SecureStorage.GetAsync("auth_token");

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            //Send PUT request
            var response = await _httpClient.PutAsync(endpoint, content);


            //Read backend response
            var result = await response.Content.ReadAsStringAsync();
            Console.WriteLine(result);


            //Throw error if failed
            if (!response.IsSuccessStatusCode)
            {
                try
                {
                    using var document = JsonDocument.Parse(result);

                    var message =
                        document.RootElement
                            .GetProperty("message")
                            .GetString();

                    throw new Exception(message);
                }
                catch (JsonException)
                {
                    throw new Exception(
                        "An unexpected error occurred When parsing the JSON.");
                }
            }
        }

        //---------------------------------------------Delete

        public async Task DeleteAsync(string endpoint)
        {
            await AddAuthHeader();

            var response =
                await _httpClient.DeleteAsync(endpoint);

            var result =
                await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                try
                {
                    using var document = JsonDocument.Parse(result);

                    var message =
                        document.RootElement
                            .GetProperty("message")
                            .GetString();

                    throw new Exception(message);
                }
                catch (JsonException)
                {
                    throw new Exception(
                        "An unexpected error occurred When parsing the JSON.");
                }
            }
        }
    }

}
