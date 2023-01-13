using Microsoft.AspNetCore.Mvc;
using Stokker.Domain.DTO;
using Stokker.Domain.Entities;
using Stokker.Infrastructure.Context;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Stokker.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvestmentController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public InvestmentController(ApplicationDbContext context)
        {
            this.context = context;
        }

        // GET: api/<InvestmentController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<InvestmentController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<InvestmentController>
        [HttpPost]
        public async Task<ActionResult> AddInvestmentToUserAccount(string id, CreateInvestmentDTO CreateInvestmentDTO)
        {
            var specificAccount = context.Accounts.Where(a => a.UserId == id).FirstOrDefault();
            var investment = new Investment()
            {
                Title = CreateInvestmentDTO.Title,
                StockTicker = CreateInvestmentDTO.StockTicker,
                BuyPrice = CreateInvestmentDTO.BuyPrice,
                PurchasedAt = CreateInvestmentDTO.PurchasedAt,
                AccountId = specificAccount.Id
            };
            await context.AddAsync(investment);
            await context.SaveChangesAsync();
            return Ok(investment);
        }

        // PUT api/<InvestmentController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<InvestmentController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
