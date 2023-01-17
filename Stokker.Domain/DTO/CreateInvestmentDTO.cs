using Stokker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stokker.Domain.DTO
{
    public class CreateInvestmentDTO
    {
        public string Title { get; set; }
        public string StockTicker { get; set; }
        public int AmountOfStocks { get; set; }
        public decimal CurrentPrice { get; set; }  
        public decimal BuyPrice { get; set; }
        public decimal? SellPrice { get; set; }
        public DateTimeOffset PurchasedAt { get; set; }
        public Guid AccountId { get; set; }
    }
}
