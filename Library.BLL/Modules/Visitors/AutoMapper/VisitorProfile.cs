using AutoMapper;
using Library.DAL.Dto.QueryCommandResult;
using Library.DAL.Models.Visitors;

namespace Library.BLL.Modules.Visitors.AutoMapper;

public class VisitorProfile : Profile
{
    public VisitorProfile()
    {
        CreateMap<Visitor, VisitorResultDto>();
    }
}
