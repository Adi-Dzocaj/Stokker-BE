using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Stokker.Infrastructure.Context;

namespace Stokker.Infrastructure.Extensions
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddPersistanceService(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<IApplicationDbContext, ApplicationDbContext>(options => options.UseSqlServer(connectionstring);
        }
    }
}