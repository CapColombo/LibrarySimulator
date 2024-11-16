using Library.BLL.Base.Command;

namespace Library.BLL.Modules.Visitors.Commands.DeleteVisitor;

public class DeleteVisitorCommand : ICommand<DeleteVisitorCommandResult>
{
    public DeleteVisitorCommand(string id)
    {
        Id = id;
    }

    public string Id { get; set; }
}
