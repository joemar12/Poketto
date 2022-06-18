using Poketto.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poketto.Infrastructure.Persistence.Seeders
{
    public interface IDataSeeder
    {
        Task SeedAsync();
    }
}
