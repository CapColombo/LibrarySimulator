using MediatR;

namespace Library.BLL.Modules.Visitors.Commands.AddOperation;

public class CommandHandler : IRequestHandler<Command, CommandResult>
{
    public Task<CommandResult> Handle(Command request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
