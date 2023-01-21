using Microsoft.AspNetCore.Mvc;
using Stokker.Domain.DTO;
using Stokker.Domain.Entities;
using Stokker.Infrastructure.Context;

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

        // PUT api/<InvestmentController>/
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
    }
}
