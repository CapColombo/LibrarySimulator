using Library.BLL.Base.Command;
using OneOf.Types;
using OneOf;

namespace Library.BLL.Modules.Visitors.Commands.DeleteVisitor;

public class DeleteVisitorCommandResult : ICommandResult<OneOf<Success, Error>>
{
    public DeleteVisitorCommandResult(OneOf<Success, Error> result)
    {
        Result = result;
    }

    public OneOf<Success, Error> Result { get; }
}
