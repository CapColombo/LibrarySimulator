using Library.BLL.Base.Command;

namespace Library.BLL.Modules.Admin.Commands.DeleteEmployee;

public class DeleteEmployeeCommand : ICommand<DeleteEmployeeCommandResult>
{
    public DeleteEmployeeCommand(string id)
    {
        Id = id;
    }

    public string Id { get; set; }
}
