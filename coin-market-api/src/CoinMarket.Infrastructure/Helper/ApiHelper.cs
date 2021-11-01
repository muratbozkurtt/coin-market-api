using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CoinMarker.Infrastructure.Request;
using CoinMarker.Infrastructure.Settings;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace CoinMarker.Infrastructure.Helper
{
    public class ApiHelper<T> : IApiHelper<T> where T:class
    {
        private readonly HttpClient _client;
        private readonly IOptions<CoinMarketApiSettings> _coinMarketApiSettings;
        public ApiHelper(IOptions<CoinMarketApiSettings> coinMarketApiSettings)
        {
            _coinMarketApiSettings = coinMarketApiSettings;
            _client = new HttpClient();
        }
        
        public async Task<IResponse> GetAsync<IResponse>(string url, string method, IRequest request)
        {
            _client.DefaultRequestHeaders.Add("X-CMC_PRO_API_KEY", _coinMarketApiSettings.Value.Key);
            _client.DefaultRequestHeaders.Add("Accepts", "application/json");
            var result =
                _client.SendAsync(
                        new HttpRequestMessage(HttpMethod.Get, url.ToString() + method.ToString())
                        {
                            Content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json")
                        })
                    .Result;

            result.EnsureSuccessStatusCode();

            var resp = await result.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<IResponse>(resp);
        }
    }
}