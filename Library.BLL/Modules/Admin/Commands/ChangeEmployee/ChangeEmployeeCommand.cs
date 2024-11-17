using Library.BLL.Base.Command;
using Library.DAL.Dto.Controllers;

namespace Library.BLL.Modules.Admin.Commands.ChangeEmployee;

public class ChangeEmployeeCommand : ICommand<ChangeEmployeeCommandResult>
{
    public ChangeEmployeeCommand(string id, EmployeeDto employeeDto)
    {
        Id = id;
        EmployeeDto = employeeDto;
    }

    public string Id { get; set; }
    public EmployeeDto EmployeeDto { get; set; }
}
