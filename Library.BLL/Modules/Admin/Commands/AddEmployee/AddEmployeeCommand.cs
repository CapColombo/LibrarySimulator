using Library.BLL.Base.Command;
using LibrarySimulator.Controllers.Dto;

namespace Library.BLL.Modules.Admin.Commands.AddEmployee;

public class AddEmployeeCommand : ICommand<AddEmployeeCommandResult>
{
    public AddEmployeeCommand(EmployeeDto employeeDto)
    {
        EmployeeDto = employeeDto;
    }

    public EmployeeDto EmployeeDto { get; set; }
}
