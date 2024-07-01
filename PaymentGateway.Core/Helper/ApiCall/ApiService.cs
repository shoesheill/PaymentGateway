using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace PaymentGateway.Core
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<T> GetAsyncResult<T>(string apiPath, HttpMethod httpMethod, Dictionary<string, string> headerParam,Dictionary<string,string> keyValuePairs,  object? requestBody = null)
        {
            try
            {
                // Set up the request headers
                _httpClient.DefaultRequestHeaders.Clear();
                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                foreach (var keyValue in headerParam)
                {
                    _httpClient.DefaultRequestHeaders.Add(keyValue.Key, keyValue.Value);
                }

                // Create the request
                //var request = new HttpRequestMessage(httpMethod, apiPath)
                //{
                //    Content = requestBody == null ? null : new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json")
                //};

                var request = new HttpRequestMessage(httpMethod, apiPath)
                {
                    Content = requestBody == null ? null : new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json")
                };
                request.Content=new FormUrlEncodedContent(keyValuePairs);
               
                // Send the request
                var response = await _httpClient.SendAsync(request);

                // Ensure the response is successful
                response.EnsureSuccessStatusCode();

                // Read and deserialize the response content
                var result = await response.Content.ReadAsStringAsync();
                if (string.IsNullOrEmpty(result))
                {
                    return default;
                }

                return JsonConvert.DeserializeObject<T>(result);
            }
            catch (HttpRequestException httpEx)
            {
                throw; // Re-throw the exception to be handled by the caller if needed
            }
            catch (JsonException jsonEx)
            {
                throw; // Re-throw the exception to be handled by the caller if needed
            }
            catch (Exception ex)
            {
                throw; // Re-throw the exception to be handled by the caller if needed
            }
        }
    }
}
