using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoinMarker.Infrastructure.Helper;
using CoinMarker.Infrastructure.Model;
using CoinMarker.Infrastructure.Request;
using CoinMarker.Infrastructure.Response;
using CoinMarker.Infrastructure.Settings;
using CoinMarket.Service.Service.Define;
using Microsoft.Extensions.Options;

namespace CoinMarket.Service.Service
{
    public class CoinService : ICoinService
    {
        private readonly IOptions<AppSettings> _appSettings;
        private readonly IOptions<CoinServiceSettings> _coinServiceSettings;
        private readonly IApiHelper<IOptions<CoinServiceSettings>> _coinService;

        public CoinService(IOptions<AppSettings> appSettings,
            IOptions<CoinServiceSettings> coinServiceSettings,
            IApiHelper<IOptions<CoinServiceSettings>> coinService)
        {
            _appSettings = appSettings;
            _coinServiceSettings = coinServiceSettings;
            _coinService = coinService;
        }

        public async Task<CoinmarketcapItemData> GetCoinsAsync(GetCoinRequest request)
        {
            var path = String.Format(_coinServiceSettings.Value.GetCoin.Path, request.Limit, request.Convert);
            var response = await _coinService.GetAsync<CoinmarketcapItemData>(_coinServiceSettings.Value.Url, 
                path, null);
            return response;
        }
    }
}