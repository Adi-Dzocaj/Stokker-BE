using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Stokker.Domain.Entities
{
    public class Investment
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string StockTicker { get; set; }
        public int AmountOfStocks { get; set; } 
        public decimal BuyPrice { get; set; }
        public decimal? SellPrice { get; set; }  
        public DateTimeOffset PurchasedAt { get; set; }
        public Account Account { get; set; }
        public Guid AccountId { get; set; }
    }
}
