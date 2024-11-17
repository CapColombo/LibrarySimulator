namespace Library.BLL.Services.ExpiredViolationWorker;

public interface IExpiredViolationWorker
{
    Task FindExpiredViolationsAsync(CancellationToken token);
}