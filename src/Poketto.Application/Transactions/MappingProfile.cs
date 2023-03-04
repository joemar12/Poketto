using AutoMapper;
using Poketto.Application.GraphQL.JournalEntries;
using Poketto.Application.Transactions.Commands;
using Poketto.Domain.Entities;

namespace Poketto.Application.Transactions;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<JournalEntryItem, JournalEntryItemDto>()
            .ForMember(dest => dest.AccountName, opt => opt.MapFrom(src => src.Account.Name));
        CreateMap<JournalEntryInput, AddJournalEntryCommand>();
        CreateMap<JournalEntryItemDto, JournalEntryItem>();
        CreateMap<AddJournalEntryCommand, JournalEntry>();
        CreateMap<JournalEntry, JournalEntryDto>();
    }
}
