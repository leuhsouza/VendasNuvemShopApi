using System.Net.Http.Headers;

namespace VendasNuvemShopApi.Services;

    public class NuvemShopService
    {
        private readonly HttpClient _httpClient;
        private readonly string _accessToken;

        public NuvemShopService(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _accessToken = config["NuvemShop:AccessToken"];
        }

        public async Task<string> BuscarVendasAsync()
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);
            var response = await _httpClient.GetAsync("https://api.nuvemshop.com.br/v1/orders");
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }
    }