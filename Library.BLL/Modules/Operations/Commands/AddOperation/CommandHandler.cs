using Library.BLL.Services.OperationObserver;
using Library.BLL.Services.OperationObserver.Interfaces;
using Library.DAL.Models.Statistic;
using MediatR;
using OneOf.Types;

namespace Library.BLL.Modules.Operations.Commands.AddOperation;

public class CommandHandler : IRequestHandler<AddOperationCommand, AddOperationCommandResult>
{
    private readonly IOperationSubject _operationData;
    private readonly IOperationObserver _operationObserver;
    private readonly IBookObserver _bookObserver;
    private readonly IRentedBookObserver _rentedBookObserver;
    private readonly IVisitorObserver _visitorObserver;
    private readonly ILoggerObserver _loggerObserver;

    public CommandHandler(
        IOperationSubject operationData,
        IOperationObserver operationObserver,
        IBookObserver bookObserver,
        IRentedBookObserver rentedBookObserver,
        IVisitorObserver visitorObserver,
        ILoggerObserver loggerObserver)
    {
        _operationData = operationData;
        _operationObserver = operationObserver;
        _bookObserver = bookObserver;
        _rentedBookObserver = rentedBookObserver;
        _visitorObserver = visitorObserver;
        _loggerObserver = loggerObserver;
    }

    public async Task<AddOperationCommandResult> Handle(AddOperationCommand request, CancellationToken cancellationToken)
    {
        _operationData.RegisterObserver(_operationObserver);
        _operationData.RegisterObserver(_bookObserver);
        _operationData.RegisterObserver(_rentedBookObserver);
        _operationData.RegisterObserver(_visitorObserver);
        _operationData.RegisterObserver(_loggerObserver);

        Operation operation = new(request.OperationDto.BookId, request.OperationDto.VisitorId, request.OperationDto.OperationType, request.OperationDto.PhysicalCondition);

        try
        {
            await _operationData.SetOperation(operation, cancellationToken);
        }
        catch (Exception)
        {
            return new AddOperationCommandResult(new Error());
        }

        return new AddOperationCommandResult(new Success());
    }
}
