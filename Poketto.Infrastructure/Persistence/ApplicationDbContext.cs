﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Poketto.Application.Common.Interfaces;
using Poketto.Domain.Entities;
using Poketto.Infrastructure.Persistence.Interceptors;
using System.Reflection;

namespace Poketto.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        private readonly AuditableEntitySaveChangesInterceptor _auditableEntitySaveChangesInterceptor;

        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options,
            AuditableEntitySaveChangesInterceptor auditableEntitySaveChangesInterceptor)
            : base(options)
        {
            _auditableEntitySaveChangesInterceptor = auditableEntitySaveChangesInterceptor;
        }
        public DbSet<Account> Accounts => Set<Account>();

        public DbSet<JournalEntry> Transactions => Set<JournalEntry>();

        public DbSet<TransactionJournal> TransactionJournals => Set<TransactionJournal>();

        public DbSet<TransactionGroup> TransactionGroups => Set<TransactionGroup>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.AddInterceptors(_auditableEntitySaveChangesInterceptor);
        }

        //public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        //{
        //    return await base.SaveChangesAsync(cancellationToken);
        //}
    }
}
