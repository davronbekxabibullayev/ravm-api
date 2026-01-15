namespace Ravm.Application.UseCases.Accounts.Mappers;

using Ravm.Application.UseCases.Accounts.Commands;
using Ravm.Application.UseCases.Accounts.Models;

public class AccountMappingProfile : Profile
{
    public AccountMappingProfile()
    {
        CreateMap<CreateEmployeeAccountCommand, User>()
            .ForMember(x => x.EmailConfirmed, y => y.MapFrom(c => false))
            .ForMember(x => x.PhoneNumberConfirmed, y => y.MapFrom(c => false));
        CreateMap<AccountModel, User>();
    }
}
