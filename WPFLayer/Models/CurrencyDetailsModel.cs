using System.Collections.Generic;

namespace WPFLayer.Models
{
    public class CurrencyDetailsModel
    {
        public string Name { get; set; }
        public string Symbol { get; set; }
        public decimal PriceInUSD { get; set; }
        public decimal TotalVolume { get; set; }
        public decimal PriceChange { get; set; }
    }
}
