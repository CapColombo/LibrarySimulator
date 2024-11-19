using AutoMapper;
using Library.BLL.Modules.Dto.ResultDto;
using Library.DAL.Models.Employees;

namespace Library.BLL.Modules.AutoMapper;

public class EmployeeProfile : Profile
{
    public EmployeeProfile()
    {
        CreateMap<Employee, EmployeeResultDto>()
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.Name));
    }
}
