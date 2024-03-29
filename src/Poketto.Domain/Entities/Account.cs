﻿using Poketto.Domain.Common;
using Poketto.Domain.Enums;

namespace Poketto.Domain.Entities;

public class Account : BaseAuditableEntity
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public AccountType AccountType { get; set; }
    public string OwnerUserId { get; set; } = string.Empty;
    public bool IsPlaceholder { get; set; }
    public IList<JournalEntryItem> JournalEntryItems { get; set; } = new List<JournalEntryItem>();
    public Guid? ParentAccountId { get; set; }
    public Account? ParentAccount { get; set; }
    public IList<Account>? ChildAccounts { get; set; }
}

