using Library.BLL.Base.Command;
using LibrarySimulator.Controllers.Dto;

namespace Library.BLL.Modules.Books.Commands.ChangeBook;

public class ChangeBookCommand : ICommand<ChangeBookCommandResult>
{
    public ChangeBookCommand(string id, BookDto bookDto)
    {
        Id = id;
        BookDto = bookDto;
    }

    public string Id { get; set; }
    public BookDto BookDto { get; set; }
}
