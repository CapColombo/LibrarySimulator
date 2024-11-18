using Library.DAL.Models.Statistic;

namespace Library.BLL.Services.OperationObserver.Interfaces;

public interface IOperationSubject
{
    public void RegisterObserver(IOperationObserver observer);

    public void RemoveObserver(IOperationObserver observer);

    public Task NotifyObserversAsync(CancellationToken token);

    public Task SetOperationAsync(Operation operation, CancellationToken token);
}
