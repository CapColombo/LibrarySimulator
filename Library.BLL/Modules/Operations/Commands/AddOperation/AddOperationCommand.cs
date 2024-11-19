using Library.BLL.Base.Command;
using LibrarySimulator.Controllers.Dto;

namespace Library.BLL.Modules.Operations.Commands.AddOperation;

public class AddOperationCommand : ICommand<AddOperationCommandResult>
{
    public AddOperationCommand(OperationDto operationDto)
    {
        OperationDto = operationDto;
    }

    public OperationDto OperationDto { get; set; }
}
