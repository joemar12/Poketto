using AutoMapper;
using Poketto.Application.Transactions.Commands;
using Poketto.Domain.Entities;

namespace Poketto.Application.Transactions
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<JournalEntry, JournalEntryDto>().ReverseMap();
            CreateMap<JournalEntryItem, JournalEntryItemDto>()
                .ForMember(x => x.AccountName, opt => opt.MapFrom(x => x.Account.Name));
            CreateMap<JournalEntryItemDto, JournalEntryItem>();
            CreateMap<AddJournalEntryCommand, JournalEntry>();
        }
    }
}
