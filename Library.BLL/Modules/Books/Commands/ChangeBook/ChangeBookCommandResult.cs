using Library.BLL.Base.Command;
using OneOf.Types;
using OneOf;

namespace Library.BLL.Modules.Books.Commands.ChangeBook;

public class ChangeBookCommandResult : ICommandResult<OneOf<Success, Error>>
{
    public ChangeBookCommandResult(OneOf<Success, Error> result)
    {
        Result = result;
    }

    public OneOf<Success, Error> Result { get; }
}
