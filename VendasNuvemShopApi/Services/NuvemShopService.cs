using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.Extensions.Configuration;

namespace VendasNuvemShopApi.Services
{
    public class NuvemShopService
    {
        private readonly HttpClient _httpClient;
        private readonly string _accessToken;
        private readonly string _userId;
        private readonly string _userAgent;

        public NuvemShopService(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _accessToken = config["NuvemShop:AccessToken"];
            _userId = config["NuvemShop:UserId"];
            _userAgent = config["NuvemShop:UserAgent"];
        }

        public async Task<string> BuscarVendasAsync()
        {
            
            // 🔍 Logs de verificação
            Console.WriteLine($"🔐 Token sendo enviado: Bearer {_accessToken}");
            Console.WriteLine($"👤 UserId: {_userId}");
            Console.WriteLine($"📇 UserAgent: {_userAgent}");
            Console.WriteLine("🔍 Token atual recebido da config: " + _accessToken);
            var request = new HttpRequestMessage(
                HttpMethod.Get,
                $"https://api.nuvemshop.com.br/2025-03/{_userId}/orders"
            );

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);
            request.Headers.UserAgent.ParseAdd(_userAgent);

            Console.WriteLine("🚀 Enviando requisição para: " + request.RequestUri);
            foreach (var header in request.Headers)
            {
                Console.WriteLine($"🧾 Header: {header.Key} => {string.Join(", ", header.Value)}");
            }

            var response = await _httpClient.SendAsync(request);

            Console.WriteLine($"📥 Status Code: {response.StatusCode}");
            var responseBody = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"📦 Body: {responseBody}");

            response.EnsureSuccessStatusCode(); // vai lançar se não for 2xx

            return responseBody;
        }
    }
}
