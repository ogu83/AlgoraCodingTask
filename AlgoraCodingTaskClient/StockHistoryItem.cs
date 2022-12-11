using System;

namespace AlgoraCodingTaskClient
{
    public class StockHistoryItem
    {
        public DateTime TimeStamp { get; set; }
        public string? Name { get; set; }
        public float Price { get; set; }
        public string PriceStr { get { return Price.ToString("n2"); } }

        public static StockHistoryItem FromStock(Stock stock, long ticks)
        {
            return new StockHistoryItem
            {
                TimeStamp = new DateTime(ticks),
                Name = stock.Name,
                Price = stock.Price
            };
        }
    }
}