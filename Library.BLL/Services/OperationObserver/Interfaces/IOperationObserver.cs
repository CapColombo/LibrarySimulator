using Library.DAL.Models.Statistic;

namespace Library.BLL.Services.OperationObserver.Interfaces;

public interface IOperationObserver
{
    Task UpdateAsync(Operation? operation, CancellationToken token);
}
