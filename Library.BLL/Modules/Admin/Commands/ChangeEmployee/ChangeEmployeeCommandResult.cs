using Library.BLL.Base.Command;
using OneOf.Types;
using OneOf;

namespace Library.BLL.Modules.Admin.Commands.ChangeEmployee;

public class ChangeEmployeeCommandResult : ICommandResult<OneOf<Success, Error>>
{
    public ChangeEmployeeCommandResult(OneOf<Success, Error> result)
    {
        Result = result;
    }

    public OneOf<Success, Error> Result { get; }
}
