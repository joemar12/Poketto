using Microsoft.Identity.Web;
using Poketto.Api.Services;
using Poketto.Application.Common.Interfaces;
using Poketto.Application.GraphQL.Accounts;
using Poketto.Application.GraphQL.JournalEntries;
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

            // Add services to the container.
            services.AddCors(options =>
            {
                options.AddPolicy("poketto-client", policy =>
                {
                    policy.WithOrigins("http://localhost:3000");
                    policy.WithHeaders("*");
                });
            });
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
                .AddRequiredScopeAuthorization()
                .AddDefaultTransactionScopeHandler()
                //.RegisterDbContext<ApplicationDbContext>()
                .AddQueryType(q => q.Name(OperationTypeNames.Query))
                .AddTypeExtension<ChartOfAccountsQueryExtension>()
                .AddTypeExtension<JournalEntriesQueryExtension>()
                .AddProjections()
                .AddFiltering()
                .AddSorting()
                .AddMutationType(m => m.Name(OperationTypeNames.Mutation))
                .AddTypeExtension<ChartOfAccountsMutationExtension>();

            return services;
        }
    }
}