using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoinMarker.Infrastructure.Helper;
using CoinMarker.Infrastructure.Model;
using CoinMarker.Infrastructure.Request;
using CoinMarker.Infrastructure.Response;
using CoinMarker.Infrastructure.Settings;
using CoinMarket.Service.Service.Define;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CoinMarket.Service.Service
{
    public class CoinService : ICoinService
    {
        private ILogger<CoinService> _logger;
        private readonly IOptions<CoinServiceSettings> _coinServiceSettings;
        private readonly IApiHelper<IOptions<CoinServiceSettings>> _coinService;

        public CoinService(ILogger<CoinService> logger,
            IOptions<AppSettings> appSettings,
            IOptions<CoinServiceSettings> coinServiceSettings,
            IApiHelper<IOptions<CoinServiceSettings>> coinService)
        {
            _logger = logger;
            _coinServiceSettings = coinServiceSettings;
            _coinService = coinService;
        }

        public async Task<CoinmarketcapItemData> GetCoinsAsync(GetCoinRequest request)
        {
            try
            {
                var path = String.Format(_coinServiceSettings.Value.GetCoin.Path, request.Limit, request.Convert);
                var response = await _coinService.GetAsync<CoinmarketcapItemData>(_coinServiceSettings.Value.Url, 
                    path, null);
                _logger.LogInformation("Get coin processing was successfully");
                return response;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }
        }
    }
}