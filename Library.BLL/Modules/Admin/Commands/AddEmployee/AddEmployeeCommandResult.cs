using Library.BLL.Base.Command;
using OneOf.Types;
using OneOf;

namespace Library.BLL.Modules.Admin.Commands.AddEmployee;

public class AddEmployeeCommandResult : ICommandResult<OneOf<Success, Error>>
{
    public AddEmployeeCommandResult(OneOf<Success, Error> result)
    {
        Result = result;
    }

    public OneOf<Success, Error> Result { get; }
}
