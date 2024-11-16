using MediatR;

namespace Library.BLL.Base.Command;

public interface ICommand<out TCommandResult> : IRequest<TCommandResult> where TCommandResult : ICommandResultBase
{
}
