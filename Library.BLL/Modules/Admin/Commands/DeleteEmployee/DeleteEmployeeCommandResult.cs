using Library.BLL.Base.Command;
using OneOf.Types;
using OneOf;

namespace Library.BLL.Modules.Admin.Commands.DeleteEmployee;

public class DeleteEmployeeCommandResult : ICommandResult<OneOf<Success, Error>>
{
    public DeleteEmployeeCommandResult(OneOf<Success, Error> result)
    {
        Result = result;
    }

    public OneOf<Success, Error> Result { get; }
}
