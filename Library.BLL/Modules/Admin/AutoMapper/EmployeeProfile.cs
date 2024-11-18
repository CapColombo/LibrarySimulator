using AutoMapper;
using Library.DAL.Dto.QueryCommandResult;
using Library.DAL.Models.Employees;

namespace Library.BLL.Modules.Admin.AutoMapper;

public class EmployeeProfile : Profile
{
    public EmployeeProfile()
    {
        CreateMap<Employee, EmployeeResultDto>()
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.Name));
    }
}
