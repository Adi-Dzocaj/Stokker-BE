using Microsoft.AspNetCore.Mvc;
using Stokker.Domain.DTO;
using Stokker.Domain.Entities;
using Stokker.Infrastructure.Context;

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
    }
}
