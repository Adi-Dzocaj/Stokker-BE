using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Stokker.Domain.Entities
{
    public class Account
    {
        public Guid Id { get; set; }
        public decimal StartingCapital { get; set; } = 0;
        public decimal AccountBalance { get; set; } = 0;
        public decimal UnusedFunds { get; set; } = 0;
        public DateTimeOffset CreationDate { get; set; }
        public ICollection<Investment> Investments { get; set; }
        public User User { get; set; }
        public string UserId { get; set; }
    }
}
