using AutoMapper;
using Library.DAL.Dto.QueryCommandResult;
using Library.DAL.Models.Statistic;

namespace Library.BLL.Modules.Statistics.AutoMapper;

public class OperationStatisticsProfile : Profile
{
    public OperationStatisticsProfile()
    {
        CreateMap<Operation, OperationStatisticsResultDto>()
            .ForMember(dest => dest.VisitorName, opt => opt.MapFrom(src => src.Visitor.Name))
            .ForMember(dest => dest.BookTitle, opt => opt.MapFrom(src => src.Book.Title));
    }
}
