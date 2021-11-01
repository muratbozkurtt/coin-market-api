using System.Collections.Generic;
using CoinMarker.Infrastructure.Model;

namespace CoinMarker.Infrastructure.Response
{
    public class CoinResponse : IResponse
    {
        public List<CoinModel> Data { get; set; }
    }
}