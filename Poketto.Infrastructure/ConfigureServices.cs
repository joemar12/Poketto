using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Poketto.Application.Common.Interfaces;
using Poketto.Infrastructure.Persistence;
using Poketto.Infrastructure.Persistence.Interceptors;
using Poketto.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poketto.Infrastructure
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<AuditableEntitySaveChangesInterceptor>();
            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                {
                    options.UseInMemoryDatabase("PokettoDb");

                });
            }
            else
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                {
                    options.UseSqlServer(
                        configuration.GetConnectionString("DefaultConnectionString"),
                        builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName));
                });
            }

            services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());
            services.AddScoped<ApplicationDbContextInitializer>();
            services.AddTransient<IDateTime, DateTimeService>();

            return services;
        }
    }
}
