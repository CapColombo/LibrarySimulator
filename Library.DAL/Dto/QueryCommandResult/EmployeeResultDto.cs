using Library.DAL.Models.Enums;

namespace Library.DAL.Dto.QueryCommandResult;

public class EmployeeResultDto
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Role { get; set; }

    public WorkSchedule WorkSchedule { get; set; }
}
