using System;
using Microsoft.EntityFrameworkCore;
using Stokker.Domain.Entities;

namespace Stokker.Infrastructure.Context
{
    public interface IApplicationDbContext
    {
        DbSet<User> Users { get; }
        DbSet<Account> Accounts { get; }
        DbSet<Investment> Investments { get; }
    }
}

