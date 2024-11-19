using Library.DAL;
using MediatR;
using OneOf.Types;

namespace Library.BLL.Modules.Books.Commands.ChangeBook;

public class CommandHandler : IRequestHandler<ChangeBookCommand, ChangeBookCommandResult>
{
    private readonly ILibraryContext _context;

    public CommandHandler(ILibraryContext context)
    {
        _context = context;
    }

    public async Task<ChangeBookCommandResult> Handle(ChangeBookCommand request, CancellationToken cancellationToken)
    {
        if (request is null || !Guid.TryParse(request.Id, out var id))
        {
            return new ChangeBookCommandResult(new Error());
        }

        var book = _context.Books.FirstOrDefault(v => v.Id == id);

        if (book == null)
        {
            return new ChangeBookCommandResult(new Error());
        }

        book.Title = request.BookDto.Title;
        book.Description = request.BookDto.Description;
        book.Authors = request.BookDto.Authors;
        book.Genres = request.BookDto.Genres;
        book.PublicationDate = request.BookDto.PublicationDate;

        _context.Update(book);
        await _context.SaveChangesAsync(cancellationToken);

        return new ChangeBookCommandResult(new Success());
    }
}
