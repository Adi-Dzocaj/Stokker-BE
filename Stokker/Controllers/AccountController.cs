using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stokker.Domain.DTO;
using Stokker.Domain.Entities;
using Stokker.Infrastructure.Context;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Stokker.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public AccountController(ApplicationDbContext context)
        {
            this.context = context;
        }
        // GET: api/<AccountController>
        [HttpGet]
        public async Task<List<Account>> GetAllAccounts()
        {
            return await context.Accounts.Include(a => a.Investments).ToListAsync();

        }

        // GET api/<AccountController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<List<Account>>> getAccountByUserId(string id)
        {
            var account = context.Accounts.Where(a => a.UserId == id).Include(a => a.Investments).FirstOrDefault();
            if (account is null)
                return NotFound("Account not found");
            return Ok(account);
        }


        // PUT api/<AccountController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<List<Account>>> UpdateAccountByUserId(string id, UpdateAccountDTO updateAccountDTO)
        {
            var specificAccount = context.Accounts.Where(a => a.UserId == id).FirstOrDefault();
            if (specificAccount is null)
                return NotFound("Account not found");

            specificAccount.StartingCapital = updateAccountDTO.StartingCapital;
            specificAccount.AccountBalance = updateAccountDTO.AccountBalance;
            specificAccount.UnusedFunds = updateAccountDTO.UnusedFunds;

            await context.SaveChangesAsync();
            return Ok(specificAccount);
        }
        // PUT api/<AccountController>/5
        [HttpPut("Investment/{id}")]
        public async Task<ActionResult<List<Account>>> UpdateAccountFundsByUserId(string id)
        {
            var specificAccount = context.Accounts.Where(a => a.UserId == id).FirstOrDefault();
            var accountInvestments = context.Investments.Where(i => i.AccountId == specificAccount.Id);

            if (specificAccount is null)
                return NotFound("Account not found");

            var unusedFunds = specificAccount.UnusedFunds;
            decimal totalInvestmentsValue = 0;
            foreach (Investment investment in accountInvestments)
            {
                totalInvestmentsValue += investment.CurrentPrice * investment.AmountOfStocks;
            }
            specificAccount.AccountBalance = totalInvestmentsValue + unusedFunds;

            await context.SaveChangesAsync();
            return Ok(specificAccount);
        }
    }
}
