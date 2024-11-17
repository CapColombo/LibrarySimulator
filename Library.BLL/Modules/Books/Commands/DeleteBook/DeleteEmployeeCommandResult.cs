using Library.BLL.Base.Command;
using OneOf;
using OneOf.Types;

namespace Library.BLL.Modules.Books.Commands.DeleteBook;

public class DeleteBookCommandResult : ICommandResult<OneOf<Success, Error>>
{
    public DeleteBookCommandResult(OneOf<Success, Error> result)
    {
        Result = result;
    }

    public OneOf<Success, Error> Result { get; }
}
