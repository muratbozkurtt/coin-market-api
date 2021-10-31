namespace CoinMarker.Infrastructure.Response
{
    public class CoinMarketResponse<T> : IResponse
    {
        public T Result { get; set; }
    }
}