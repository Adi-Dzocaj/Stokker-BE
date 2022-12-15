using System;
using Microsoft.Extensions.DependencyInjection;
using Stokker.Domain.Services;
using Stokker.Domain.Services.Interfaces;

namespace Stokker.Domain.Extensions
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddDomainServices(this IServiceCollection services)
        => services.AddTransient<ITestService, TestService>();
    }
}

