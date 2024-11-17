using Library.BLL.Services.ExpiredViolationWorker;
using Microsoft.Extensions.Hosting;

namespace Library.BLL.HostedServices;

public class ExpiredViolationWorkerHostedService : IHostedService
{
    private Timer? _timer;
    private readonly IExpiredViolationWorker _worker;

    public ExpiredViolationWorkerHostedService(IExpiredViolationWorker worker)
    {
        _worker = worker;
    }

    public Task StartAsync(CancellationToken token)
    {
        DateTime midnight = DateTime.UtcNow.AddDays(1).Date;
        _timer = new Timer((object? state) => _worker.FindExpiredViolationsAsync(token), null, midnight - DateTime.UtcNow,
        TimeSpan.FromHours(24));

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken token)
    {
        _timer?.Change(Timeout.Infinite, 0);
        return Task.CompletedTask;
    }
}
