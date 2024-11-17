using Library.BLL.Base.Command;

namespace Library.BLL.Modules.Books.Commands.DeleteBook;

public class DeleteBookCommand : ICommand<DeleteBookCommandResult>
{
    public DeleteBookCommand(string id)
    {
        Id = id;
    }

    public string Id { get; set; }
}
