using Library.BLL.Base.Command;
using OneOf.Types;
using OneOf;

namespace Library.BLL.Modules.Operations.Commands.AddOperation;

public class AddOperationCommandResult : ICommandResult<OneOf<Success, Error>>
{
    public AddOperationCommandResult(OneOf<Success, Error> result)
    {
        Result = result;
    }

    public OneOf<Success, Error> Result { get; }
}
