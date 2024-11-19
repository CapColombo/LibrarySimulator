using Library.DAL.Models.Employees;
using Library.DAL.Models.Enums;

namespace LibrarySimulator.Controllers.Dto;

public class EmployeeDto
{
    public string Name { get; set; }

    public Role Role { get; set; }

    public WorkSchedule WorkSchedule { get; set; }
}
