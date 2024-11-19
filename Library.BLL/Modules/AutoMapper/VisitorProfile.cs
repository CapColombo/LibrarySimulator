using AutoMapper;
using Library.BLL.Modules.Dto.ResultDto;
using Library.DAL.Models.Visitors;

namespace Library.BLL.Modules.AutoMapper;

public class VisitorProfile : Profile
{
    public VisitorProfile()
    {
        CreateMap<Visitor, VisitorResultDto>();
    }
}
