using Library.BLL.Base.Command;
using Library.DAL.Dto.Controllers;

namespace Library.BLL.Modules.Visitors.Commands.AddOperation;

public class Command : ICommand<CommandResult>
{
    public Command(VisitorDto visitorDto)
    {
        VisitorDto = visitorDto;
    }

    public VisitorDto VisitorDto { get; set; }
}
