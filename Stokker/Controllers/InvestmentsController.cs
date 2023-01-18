using Microsoft.AspNetCore.Mvc;
using Stokker.Domain.DTO;
using Stokker.Domain.Entities;
using Stokker.Infrastructure.Context;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Stokker.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvestmentsController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public InvestmentsController(ApplicationDbContext context)
        {
            this.context = context;
        }
        // GET: api/<InvestmentsController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<InvestmentsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<InvestmentsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<InvestmentsController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Investment>> UpdateInvestmentById(Guid id, UpdateInvestmentDTO updateInvestmentDTO)
        {
            var userInvestment = context.Investments.Where(i => i.Id == id).FirstOrDefault();

            if (userInvestment != null) {
                userInvestment.CurrentPrice = updateInvestmentDTO.CurrentPrice;
                await context.SaveChangesAsync();
                return Ok(userInvestment);
            } else
            {
                return new EmptyResult();
            }
        }

        // DELETE api/<InvestmentsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
