using Library.BLL.Base.Command;
using OneOf.Types;
using OneOf;

namespace Library.BLL.Modules.Visitors.Commands.AddVisitor;

public class AddVisitorCommandResult : ICommandResult<OneOf<Success, Error>>
{
    public AddVisitorCommandResult(OneOf<Success, Error> result)
    {
        Result = result;
    }

    public OneOf<Success, Error> Result { get; }
}
