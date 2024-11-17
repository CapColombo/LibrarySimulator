using Library.BLL.Base.Command;
using OneOf.Types;
using OneOf;

namespace Library.BLL.Modules.Books.Commands.AddBook;

public class AddBookCommandResult : ICommandResult<OneOf<Success, Error>>
{
    public AddBookCommandResult(OneOf<Success, Error> result)
    {
        Result = result;
    }

    public OneOf<Success, Error> Result { get; }
}
