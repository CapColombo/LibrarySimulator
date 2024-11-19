using Library.BLL.Base.Command;
using LibrarySimulator.Controllers.Dto;

namespace Library.BLL.Modules.Visitors.Commands.ChangeVisitor;

public class ChangeVisitorCommand : ICommand<ChangeVisitorCommandResult>
{
    public ChangeVisitorCommand(string id, VisitorDto visitorDto)
    {
        Id = id;
        VisitorDto = visitorDto;
    }

    public string Id { get; set; }
    public VisitorDto VisitorDto { get; set; }
}
