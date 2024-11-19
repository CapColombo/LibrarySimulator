using Library.DAL;
using MediatR;
using OneOf.Types;

namespace Library.BLL.Modules.Books.Commands.DeleteBook;

public class CommandHandler : IRequestHandler<DeleteBookCommand, DeleteBookCommandResult>
{
    private readonly ILibraryContext _context;

    public CommandHandler(ILibraryContext context)
    {
        _context = context;
    }

    public async Task<DeleteBookCommandResult> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
    {
        if (request is null || !Guid.TryParse(request.Id, out var id))
        {
            return new DeleteBookCommandResult(new Error());
        }

        var book = _context.Books.FirstOrDefault(v => v.Id == id);

        if (book == null)
        {
            return new DeleteBookCommandResult(new Error());
        }

        _context.Remove(book);
        await _context.SaveChangesAsync(cancellationToken);

        return new DeleteBookCommandResult(new Success());
    }
}
