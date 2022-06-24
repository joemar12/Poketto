using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity.Web;
using Poketto.Api.Services;
using Poketto.Application.Common.Interfaces;
using Poketto.Application.GraphQL.Queries;
using Poketto.Application.GraphQL.Queries.Accounts;
using Poketto.Application.GraphQL.Queries.Transactions;
using Poketto.Application.GraphQL.Security.Extensions;
using Poketto.Infrastructure.Persistence;

namespace Poketto.Api
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddWebApi(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDatabaseDeveloperPageExceptionFilter();
            services.AddHttpContextAccessor();
            services.AddSingleton<ICurrentUserService, CurrentUserService>();
            services.AddHealthChecks()
                .AddDbContextCheck<ApplicationDbContext>();


            var debugAzureAdConfig = configuration.GetSection("AzureAd").GetChildren();

            // Add services to the container.
            services.AddCors(options => options.AddPolicy("allowAny", o => o.AllowAnyOrigin()));
            services.AddMicrosoftIdentityWebApiAuthentication(configuration);
            services.AddAuthorization();
            services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            return services;
        }

        public static IServiceCollection AddGraphQLServices(this IServiceCollection services)
        {
            services.AddGraphQLServer()
                .AddAuthorization()
                .AddRequiredScopesAuthorization()
                .RegisterDbContext<ApplicationDbContext>()
                .AddQueryType(q => q.Name(OperationTypeNames.Query))
                .AddTypeExtension<ChartOfAccountsQueryExtensions>()
                .AddTypeExtension<TransactionsQueryExtensions>()
                .AddProjections()
                .AddFiltering()
                .AddSorting();

            return services;
        }
    }
}
