using System.Collections.Generic;
using System.Threading.Tasks;
using CoinMarker.Infrastructure.Model;
using CoinMarker.Infrastructure.Request;
using CoinMarker.Infrastructure.Response;

namespace CoinMarket.Service.Service.Define
{
    public interface ICoinService
    {
        Task<CoinmarketcapItemData> GetCoinsAsync(GetCoinRequest request);
    }
}