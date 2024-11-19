using Library.BLL.Base.Command;
using LibrarySimulator.Controllers.Dto;

namespace Library.BLL.Modules.Visitors.Commands.AddVisitor;

public class AddVisitorCommand : ICommand<AddVisitorCommandResult>
{
    public AddVisitorCommand(VisitorDto visitorDto)
    {
        VisitorDto = visitorDto;
    }

    public VisitorDto VisitorDto { get; set; }
}
