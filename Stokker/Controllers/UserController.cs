using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Stokker.Domain.Entities;
using Stokker.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Stokker.Infrastructure.Context;
using Stokker.Domain.DTO;

namespace Stokker.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public UserController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<List<User>> GetAllUsers()
        {
            return await context.Users.Include(u => u.Account).ThenInclude(a => a.Investments).ToListAsync();

        }
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> getSpecificUser(string id)
        {
            var user = context.Users.Where(u => u.Id == id).Include(u => u.Account).ThenInclude(a => a.Investments).FirstOrDefault();
            if (user is null)
                return NotFound("Account not found");
            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult> AddUser(CreateUserDTO createUserDTO)
        {
            var user = new User()
            {
                Id = createUserDTO.Id,
                Email = createUserDTO.Email,
                Account = new Account() {
                    AccountBalance = 0,
                    UnusedFunds = 0,
                    UserId = createUserDTO.Id
                }
            };
            await context.AddAsync(user);
            await context.SaveChangesAsync();
            return Ok(user);
        }
    }
}

