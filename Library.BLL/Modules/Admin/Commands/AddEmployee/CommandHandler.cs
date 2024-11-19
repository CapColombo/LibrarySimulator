using Library.DAL;
using Library.DAL.Models.Employees;
using MediatR;
using OneOf.Types;

namespace Library.BLL.Modules.Admin.Commands.AddEmployee;

public class CommandHandler : IRequestHandler<AddEmployeeCommand, AddEmployeeCommandResult>
{
    private readonly ILibraryContext _context;

    public CommandHandler(ILibraryContext context)
    {
        _context = context;
    }

    public async Task<AddEmployeeCommandResult> Handle(AddEmployeeCommand request, CancellationToken cancellationToken)
    {
        if (request == null)
        {
            return new AddEmployeeCommandResult(new Error());
        }

        Employee newEmployee = new(request.EmployeeDto.Name, request.EmployeeDto.Role, request.EmployeeDto.WorkSchedule);

        await _context.AddAsync(newEmployee, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return new AddEmployeeCommandResult(new Success());
    }
}
