using Library.BLL.Modules.Visitors.Commands.ChangeVisitor;
using Library.DAL;
using Library.DAL.Models.Employees;
using MediatR;
using OneOf.Types;

namespace Library.BLL.Modules.Admin.Commands.ChangeEmployee;

public class CommandHandler : IRequestHandler<ChangeEmployeeCommand, ChangeEmployeeCommandResult>
{
    private readonly ILibraryContext _context;

    public CommandHandler(ILibraryContext context)
    {
        _context = context;
    }

    public async Task<ChangeEmployeeCommandResult> Handle(ChangeEmployeeCommand request, CancellationToken cancellationToken)
    {
        if (request is null || !Guid.TryParse(request.Id, out var id))
        {
            return new ChangeEmployeeCommandResult(new Error());
        }

        var employee = _context.Employees.FirstOrDefault(v => v.Id == id);

        if (employee == null)
        {
            return new ChangeEmployeeCommandResult(new Error());
        }

        employee.Name = request.EmployeeDto.Name;
        employee.Role = request.EmployeeDto.Role;
        employee.WorkSchedule = request.EmployeeDto.WorkSchedule;

        _context.Update(employee);
        await _context.SaveChangesAsync(cancellationToken);

        return new ChangeEmployeeCommandResult(new Success());
    }
}
