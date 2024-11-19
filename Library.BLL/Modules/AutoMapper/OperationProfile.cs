using AutoMapper;
using Library.BLL.Modules.Dto.ResultDto;
using Library.DAL.Models.Statistic;

namespace Library.BLL.Modules.AutoMapper;

public class OperationProfile : Profile
{
    public OperationProfile()
    {
        CreateMap<Operation, OperationResultDto>()
            .ForMember(dest => dest.VisitorName, opt => opt.MapFrom(src => src.Visitor.Name));
    }
}
