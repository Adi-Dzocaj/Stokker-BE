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
        public async Task<ActionResult> AddInvestmentToUserAccountSubtractFromAccount(string id, CreateInvestmentDTO CreateInvestmentDTO)
        {
            var specificAccount = context.Accounts.Where(a => a.UserId == id).FirstOrDefault();
            var investment = new Investment()
            {
                Title = CreateInvestmentDTO.Title,
                StockTicker = CreateInvestmentDTO.StockTicker,
                AmountOfStocks = CreateInvestmentDTO.AmountOfStocks,
                CurrentPrice = CreateInvestmentDTO.CurrentPrice,
                BuyPrice = CreateInvestmentDTO.BuyPrice,
                PurchasedAt = CreateInvestmentDTO.PurchasedAt,
                AccountId = specificAccount.Id
            };

            if (investment.AmountOfStocks * investment.BuyPrice < specificAccount.UnusedFunds)
            {
                await context.AddAsync(investment);
                // Subtract the purchase amount from the unusedFunds column in the user's account
                specificAccount.UnusedFunds = specificAccount.UnusedFunds - investment.AmountOfStocks * investment.BuyPrice;
                await context.SaveChangesAsync();
                return Ok(investment);
            }
            else
            {
                return new EmptyResult();
            }
        }

        // PUT api/<InvestmentController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateInvestmentFromUserAccountIncreaseUnusedFunds(Guid id, UpdateInvestmentDTO updateInvestmentDTO)
        {

            var specificInvestment = context.Investments.Where(i => i.Id == id).FirstOrDefault();
            var accountId = context.Investments.Where(i => i.Id == id).FirstOrDefault().AccountId;
            var specificAccount = context.Accounts.Where(a => a.Id == accountId).FirstOrDefault();

            if (specificInvestment != null)
            {
                specificInvestment.AmountOfStocks = specificInvestment.AmountOfStocks - updateInvestmentDTO.AmountOfStocks;
                specificAccount.UnusedFunds = specificAccount.UnusedFunds + updateInvestmentDTO.AmountOfStocks * specificInvestment.CurrentPrice;
                if (specificInvestment.AmountOfStocks == 0) {
                    context.Investments.Remove(specificInvestment);
                }
                await context.SaveChangesAsync();
                return Ok(specificInvestment);
            }
            else
            {
                return new EmptyResult();
            }
        }

        // DELETE api/<InvestmentController>/5
        //[HttpDelete("{id}")]
        //public async Task<ActionResult> DeleteInvestmentFromUserAccountIncreaseUnusedFunds(Guid id)
        //{

        //    var specificInvestment = context.Investments.Where(i => i.Id == id).FirstOrDefault();
        //    var accountId = context.Investments.Where(i => i.Id == id).FirstOrDefault().AccountId;
        //    var specificAccount = context.Accounts.Where(a => a.Id == accountId).FirstOrDefault();
        //    if (specificInvestment != null)
        //    {
        //        context.Investments.Remove(specificInvestment);
        //        specificAccount.UnusedFunds = specificAccount.UnusedFunds + specificInvestment.AmountOfStocks * specificInvestment.BuyPrice;
        //        await context.SaveChangesAsync();
        //        return Ok(specificInvestment);
        //    }
        //    else
        //    {
        //        return new EmptyResult();
        //    }
        //}
    }
}
