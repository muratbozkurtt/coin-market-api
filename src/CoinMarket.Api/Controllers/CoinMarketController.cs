using System.Collections.Generic;
using System.Threading.Tasks;
using CoinMarker.Infrastructure.Model;
using CoinMarker.Infrastructure.Request;
using CoinMarker.Infrastructure.Response;
using CoinMarket.Service.Service.Define;
using Microsoft.AspNetCore.Mvc;

namespace CoinMarket.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CoinMarketController : BaseController
    {
        private ICoinService _coinService;

        public CoinMarketController(ICoinService coinService)
        {
            _coinService = coinService;
        }
        
        [HttpGet("getCoin")]
        public async Task<CoinmarketcapItemData> GetAllUsersByTypeAsync([FromQuery]GetCoinRequest request)
        {
            return await _coinService.GetCoinsAsync(request);
        }

    }
}