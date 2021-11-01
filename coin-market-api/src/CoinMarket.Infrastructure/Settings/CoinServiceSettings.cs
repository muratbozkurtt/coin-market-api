namespace CoinMarker.Infrastructure.Settings
{
    public class CoinServiceSettings : ISetting
    {
        public ApiMethod GetCoin { get; set; }
        public string Url { get; set; }
    }
}