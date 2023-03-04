using Poketto.Application.Common.Interfaces;

namespace Poketto.Infrastructure.Services;

public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.Now;
}
