using Library.DAL;
using MediatR;
using OneOf.Types;

namespace Library.BLL.Modules.Admin.Commands.DeleteEmployee;

public class CommandHandler : IRequestHandler<DeleteEmployeeCommand, DeleteEmployeeCommandResult>
{
    private readonly ILibraryContext _context;

    public CommandHandler(ILibraryContext context)
    {
        _context = context;
    }

    public async Task<DeleteEmployeeCommandResult> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
    {
        if (request is null || !Guid.TryParse(request.Id, out var id))
        {
            return new DeleteEmployeeCommandResult(new Error());
        }

        var employee = _context.Employees.FirstOrDefault(v => v.Id == id);

        if (employee == null)
        {
            return new DeleteEmployeeCommandResult(new Error());
        }

        await _context.RemoveWithSaveAsync(employee, cancellationToken);

        return new DeleteEmployeeCommandResult(new Success());
    }
}
