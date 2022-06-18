using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Poketto.Application.Common.Interfaces;
using Poketto.Domain.Common;

namespace Poketto.Infrastructure.Persistence.Interceptors
{
    public class AuditableEntitySaveChangesInterceptor : SaveChangesInterceptor
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IDateTime _dateTime;

        public AuditableEntitySaveChangesInterceptor(
            ICurrentUserService currentUserService, 
            IDateTime dateTime)
        {
            _currentUserService = currentUserService;
            _dateTime = dateTime;
        }

        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            return base.SavingChanges(eventData, result);
        }

        public void UpdateAuditProperties(DbContext? context)
        {
            if (context != null)
            {
                var currentUser = _currentUserService.GetCurrentUser();
                foreach (var entry in context.ChangeTracker.Entries<BaseAuditableEntity>())
                {
                    if (entry.State == EntityState.Added)
                    {
                        entry.Entity.CreatedBy = currentUser;
                        entry.Entity.Created = _dateTime.Now;
                    }

                    if (entry.State == EntityState.Added || entry.State == EntityState.Modified || entry.HasChangedOwnedEntities())
                    {
                        entry.Entity.LastModifiedBy = currentUser;
                        entry.Entity.LastModified = _dateTime.Now;
                    }
                }
            }
        }
    }
}
