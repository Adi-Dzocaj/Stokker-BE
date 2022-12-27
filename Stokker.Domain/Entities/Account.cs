using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stokker.Domain.Entities
{
    public class Account
    {
        public Guid Id { get; set; }
        public decimal UnusedFunds { get; set; } = 0;
        public ICollection<Investment> Investments { get; set; }
        public User User { get; set; }
        public Guid UserId { get; set; }
    }
}
