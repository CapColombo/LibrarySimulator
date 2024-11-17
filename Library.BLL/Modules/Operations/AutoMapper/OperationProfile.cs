using AutoMapper;
using Library.DAL.Dto.QueryCommandResult;
using Library.DAL.Models.Statistic;

namespace Library.BLL.Modules.Operations.AutoMapper;

public class OperationProfile : Profile
{
    public OperationProfile()
    {
        CreateMap<Operation, OperationResultDto>()
            .ForMember(dest => dest.VisitorName, opt => opt.MapFrom(src => src.Visitor.Name));
    }
}
