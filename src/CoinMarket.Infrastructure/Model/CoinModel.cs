using System;
using System.Collections.Generic;
using CoinMarker.Infrastructure.Response;

namespace CoinMarker.Infrastructure.Model
{
    public class CoinModel : IResponse
    {
        public CoinModel()
        {
            Tags = new List<string>();
            Quote = new Dictionary<string, QuoteModel>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
        public string Slug { get; set; }
        public int Num_Market_Pairs { get; set; }
        public DateTime Date_Added { get; set; }
        public List<string> Tags { get; set; }
        public int Mux_Supply { get; set; }
        public int Circulating_Supply { get; set; }
        public int Total_Supply { get; set; }
        public string Platform { get; set; }
        public string Cmc_Rank { get; set; }
        public DateTime Last_Updated { get; set; }
        public Dictionary<string, QuoteModel> Quote { get; set; }
    }

    public class QuoteModel
    {
        public decimal Price { get; set; }
        public decimal Volume_24h { get; set; }
        public decimal Volume_Change_24h { get; set; }
        public decimal Percent_Change_1h { get; set; }
        public decimal Percent_Change_24h { get; set; }
        public decimal Percent_Change_7d { get; set; }
        public decimal Percent_Change_30d { get; set; }
        public decimal Percent_Change_60d { get; set; }
        public decimal Market_Cap { get; set; }
        public decimal Market_Cap_Dominance { get; set; }
        public decimal Fully_Diluted_Market_Cap { get; set; }
        public DateTime Last_Updated { get; set; }
    }
}