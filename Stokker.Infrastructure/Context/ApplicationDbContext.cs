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
            builder.Entity<User>().HasOne(u => u.Account).WithOne(a => a.User).HasForeignKey<Account>(a => a.UserId);
            builder.Entity<Account>().HasOne(a => a.User).WithOne(u => u.Account).HasForeignKey<User>(u => u.AccountId);
            // builder.Entity<Investment>().HasOne(i => i.Account).WithMany(a => a.Investments);

            base.OnModelCreating(builder);
        }
    }
}
