using Library.BLL.Base.Command;
using Library.DAL.Dto.Controllers;

namespace Library.BLL.Modules.Admin.Commands.AddEmployee;

public class AddEmployeeCommand : ICommand<AddEmployeeCommandResult>
{
    public AddEmployeeCommand(EmployeeDto employeeDto)
    {
        EmployeeDto = employeeDto;
    }

    public EmployeeDto EmployeeDto { get; set; }
}
