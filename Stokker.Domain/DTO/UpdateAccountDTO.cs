using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stokker.Domain.DTO
{
    public class UpdateAccountDTO
    {
        public decimal AccountBalance { get; set; }
        public decimal UnusedFunds { get; set; }
    }
}
