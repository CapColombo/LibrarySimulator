using Library.DAL;
using Library.DAL.Models.Visitors;
using MediatR;
using OneOf.Types;

namespace Library.BLL.Modules.Visitors.Commands.AddVisitor;

public class CommandHandler : IRequestHandler<AddVisitorCommand, AddVisitorCommandResult>
{
    private readonly ILibraryContext _context;

    public CommandHandler(ILibraryContext context)
    {
        _context = context;
    }

    public async Task<AddVisitorCommandResult> Handle(AddVisitorCommand request, CancellationToken cancellationToken)
    {
        if (request == null)
        {
            return new AddVisitorCommandResult(new Error());
        }

        Visitor newVisitor = new(request.VisitorDto.Name, request.VisitorDto.Email);

        await _context.AddAsync(newVisitor, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return new AddVisitorCommandResult(new Success());
    }
}
