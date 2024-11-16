using Library.BLL.Base.Command;
using OneOf.Types;
using OneOf;

namespace Library.BLL.Modules.Visitors.Commands.AddOperation;

public class CommandResult : ICommandResult<OneOf<Success, Error>>
{
    public CommandResult(OneOf<Success, Error> result)
    {
        Result = result;
    }

    public OneOf<Success, Error> Result { get; }
}
