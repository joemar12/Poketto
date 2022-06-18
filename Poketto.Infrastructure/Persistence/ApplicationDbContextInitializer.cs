using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Poketto.Domain.Entities;
using Poketto.Infrastructure.Persistence.Seeders;
using System.Reflection;

namespace Poketto.Infrastructure.Persistence
{
    public class ApplicationDbContextInitializer
    {
        private readonly ILogger<ApplicationDbContextInitializer> _logger;
        private readonly ApplicationDbContext _context;

        public ApplicationDbContextInitializer(ILogger<ApplicationDbContextInitializer> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task InitializeAsync()
        {
            try
            {
                if (_context.Database.IsSqlServer())
                {
                    await _context.Database.MigrateAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while initialising the database.");
                throw;
            }
        }

        public async Task SeedAsync()
        {
            try
            {
                await TrySeedAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while seeding the database.");
                throw;
            }
        }

        public async Task TrySeedAsync()
        {
            var seeders = GetSeeders();
            foreach (var seeder in seeders)
            {
                await seeder.SeedAsync();
            }
        }

        private IEnumerable<IDataSeeder> GetSeeders()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var types = assembly.GetTypes();
            var seederTypes = from type in types
                              where type != null
                              && type.GetInterface(typeof(IDataSeeder).Name) == typeof(IDataSeeder)
                              && !type.IsAbstract
                              select type;
            var seederInstances = new List<IDataSeeder>();
            foreach (var seederType in seederTypes)
            {
                var instance = Activator.CreateInstance(seederType, _context) as IDataSeeder;
                if (instance != null)
                {
                    seederInstances.Add(instance);
                }
            }
            return seederInstances;
        }
    }
}
