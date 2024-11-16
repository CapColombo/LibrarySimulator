using Library.DAL.Models.Employees;
using Library.DAL.Models.Enums;

namespace Library.DAL.Dto.Controllers;

public class EmployeeDto
{
    public string Name { get; set; }

    public Role Role { get; set; }

    public WorkSchedule WorkSchedule { get; set; }
}
