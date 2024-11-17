using Library.DAL;
using Library.DAL.Models.Books;
using MediatR;
using OneOf.Types;

namespace Library.BLL.Modules.Books.Commands.AddBook;

public class CommandHandler : IRequestHandler<AddBookCommand, AddBookCommandResult>
{
    private readonly ILibraryContext _context;

    public CommandHandler(ILibraryContext context)
    {
        _context = context;
    }

    public async Task<AddBookCommandResult> Handle(AddBookCommand request, CancellationToken cancellationToken)
    {
        if (request == null)
        {
            return new AddBookCommandResult(new Error());
        }

        Book newBook = new(request.BookDto.Title, request.BookDto.Description, request.BookDto.Authors, request.BookDto.Genres, request.BookDto.PublicationDate);

        await _context.AddWithSaveAsync(newBook, cancellationToken);

        return new AddBookCommandResult(new Success());
    }
}
