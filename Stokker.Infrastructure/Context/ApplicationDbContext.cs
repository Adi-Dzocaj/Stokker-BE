using System;
using Microsoft.EntityFrameworkCore;
using Stokker.Domain.Entities;
using Stokker.Infrastructure.Extensions;

namespace Stokker.Infrastructure.Context
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Investment> Investments { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>().HasOne(a => a.Account).WithOne(a => a.User).HasForeignKey<Account>(b => b.UserId);
            builder.Entity<Account>().HasOne(a => a.User).WithOne(a => a.Account).HasForeignKey<User>(b => b.AccountId);

            base.OnModelCreating(builder);
        }
    }
}
