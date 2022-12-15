using System;
using Microsoft.EntityFrameworkCore;
using Stokker.Domain.Entities;
using Stokker.Infrastructure.Extensions;

namespace Stokker.Infrastructure.Context
{
    public class ApplicationDbContext : DbContext, IApplicationDBContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Test> Tests { get; set; }
    }
}

