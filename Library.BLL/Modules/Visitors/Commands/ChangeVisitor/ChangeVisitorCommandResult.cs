using Library.BLL.Base.Command;
using OneOf.Types;
using OneOf;

namespace Library.BLL.Modules.Visitors.Commands.ChangeVisitor;

public class ChangeVisitorCommandResult : ICommandResult<OneOf<Success, Error>>
{
    public ChangeVisitorCommandResult(OneOf<Success, Error> result)
    {
        Result = result;
    }

    public OneOf<Success, Error> Result { get; }
}
