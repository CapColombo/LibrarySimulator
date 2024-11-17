using Library.BLL.Services.OperationObserver.Interfaces;
using Library.DAL.Models.Statistic;

namespace Library.BLL.Services.OperationObserver;

public class OperationData : IOperationSubject
{
    private readonly List<IOperationObserver> _observers = [];
    private Operation? _operation = null;

    public async Task SetOperation(Operation operation, CancellationToken token)
    {
        _operation = operation;
        await NotifyObserversAsync(token);
    }

    public async Task NotifyObserversAsync(CancellationToken token)
    {
        foreach (var observer in _observers)
        {
            await observer.UpdateAsync(_operation, token);
        }
    }

    public void RegisterObserver(IOperationObserver observer)
    {
        if (!_observers.Contains(observer))
        {
            _observers.Add(observer);
        }
    }

    public void RemoveObserver(IOperationObserver observer)
    {
        _observers.Remove(observer);
    }
}
