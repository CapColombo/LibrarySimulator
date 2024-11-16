using Library.DAL;
using MediatR;
using OneOf.Types;

namespace Library.BLL.Modules.Visitors.Commands.ChangeVisitor;

public class CommandHandler : IRequestHandler<ChangeVisitorCommand, ChangeVisitorCommandResult>
{
    private readonly ILibraryContext _context;

    public CommandHandler(ILibraryContext context)
    {
        _context = context;
    }

    public async Task<ChangeVisitorCommandResult> Handle(ChangeVisitorCommand request, CancellationToken cancellationToken)
    {
        if (request is null || !Guid.TryParse(request.Id, out var id))
        {
            return new ChangeVisitorCommandResult(new Error());
        }

        var visitor = _context.Visitors.FirstOrDefault(v =>  v.Id == id);

        if (visitor == null)
        {
            return new ChangeVisitorCommandResult(new Error());
        }

        visitor.Name = request.VisitorDto.Name;
        visitor.Email = request.VisitorDto.Email;

        await _context.UpdateWithSaveAsync(visitor, cancellationToken);

        return new ChangeVisitorCommandResult(new Success());
    }
}
