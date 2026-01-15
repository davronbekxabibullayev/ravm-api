namespace Ravm.Application.UseCases.Reasons.Mappers;

using Ravm.Application.UseCases.Reasons.Commands;
using Ravm.Application.UseCases.Reasons.Models;

public class ReasonMappingProfile : Profile
{
    public ReasonMappingProfile()
    {
        CreateMap<Reason, ReasonModel>();
        CreateMap<CreateReasonCommand, Reason>();
        CreateMap<UpdateReasonCommand, Reason>();
    }
}
