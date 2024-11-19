using Library.BLL.Base.Command;
using LibrarySimulator.Controllers.Dto;

namespace Library.BLL.Modules.Books.Commands.AddBook;

public class AddBookCommand : ICommand<AddBookCommandResult>
{
    public AddBookCommand(BookDto bookDto)
    {
        BookDto = bookDto;
    }

    public BookDto BookDto { get; set; }
}
