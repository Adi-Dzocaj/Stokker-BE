using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Stokker.Domain.Entities;
using Stokker.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Stokker.Infrastructure.Context;
using Stokker.Domain.DTO;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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
        public string Get(int id)
        {
            return "value";
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

