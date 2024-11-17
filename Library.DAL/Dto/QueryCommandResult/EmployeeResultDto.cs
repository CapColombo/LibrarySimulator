using Library.DAL.Models.Employees;
using Library.DAL.Models.Enums;

namespace Library.DAL.Dto.QueryCommandResult;

public class EmployeeResultDto
{
    public Guid Id { get; }

    public string Name { get; set; }

    public Role Role { get; set; }

    public WorkSchedule WorkSchedule { get; set; }

    public static implicit operator EmployeeResultDto?(VisitorResultDto? v)
    {
        throw new NotImplementedException();
    }
}
