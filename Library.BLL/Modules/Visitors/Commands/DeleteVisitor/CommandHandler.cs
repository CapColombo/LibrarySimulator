using Library.DAL;
using MediatR;
using OneOf.Types;

namespace Library.BLL.Modules.Visitors.Commands.DeleteVisitor;

public class CommandHandler : IRequestHandler<DeleteVisitorCommand, DeleteVisitorCommandResult>
{
    private readonly ILibraryContext _context;

    public CommandHandler(ILibraryContext context)
    {
        _context = context;
    }

    public async Task<DeleteVisitorCommandResult> Handle(DeleteVisitorCommand request, CancellationToken cancellationToken)
    {
        if (request is null || !Guid.TryParse(request.Id, out var id))
        {
            return new DeleteVisitorCommandResult(new Error());
        }

        var visitor = _context.Visitors.FirstOrDefault(v => v.Id == id);

        if (visitor == null)
        {
            return new DeleteVisitorCommandResult(new Error());
        }

        await _context.RemoveWithSaveAsync(visitor, cancellationToken);

        return new DeleteVisitorCommandResult(new Success());
    }
}
