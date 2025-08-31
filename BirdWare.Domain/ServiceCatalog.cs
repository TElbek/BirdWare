using BirdWare.Domain.Entities;
using BirdWare.Domain.Interfaces;
using BirdWare.Domain.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace BirdWare.Domain
{
    [ExcludeFromCodeCoverage]
    public static class ServiceCatalog
    {
        public static IServiceCollection RegisterDomain(this IServiceCollection services)
        {
            services.AddTransient<IPasswordHelper, PasswordHelper>();
            services.AddTransient<ITokenHelper, TokenHelper>();
            services.AddTransient<IPasswordHasher<Bruger>, PasswordHasher<Bruger>>();
            return services;
        }
    }
}
