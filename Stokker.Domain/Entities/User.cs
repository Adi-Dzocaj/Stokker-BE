using System;
namespace Stokker.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Account Account { get; set; }
        public Guid AccountId { get; set; } 
    }
}