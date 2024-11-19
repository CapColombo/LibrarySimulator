using Library.BLL.HostedServices;
using Library.BLL.Modules.AutoMapper;
using Library.BLL.Services.ExpiredViolationWorker;
using Library.BLL.Services.OperationObserver;
using Library.BLL.Services.OperationObserver.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Library.BLL;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddDependencies(this IServiceCollection services)
    {
        services.AddAutoMapperDependencies();
        services.AddOperationObserverDependencies();
        services.AddSingleton<IExpiredViolationWorker, ExpiredViolationWorker>();
        services.AddHostedService<ExpiredViolationWorkerHostedService>();
        return services;
    }

    private static IServiceCollection AddAutoMapperDependencies(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(OperationProfile));
        services.AddAutoMapper(typeof(BookProfile));
        services.AddAutoMapper(typeof(VisitorProfile));
        services.AddAutoMapper(typeof(EmployeeProfile));
        services.AddAutoMapper(typeof(OperationStatisticsProfile));
        return services;
    }

    private static IServiceCollection AddOperationObserverDependencies(this IServiceCollection services)
    {
        services.AddScoped<IOperationSubject, OperationData>();
        services.AddScoped<IOperationObserver, OperationObserver>();
        services.AddScoped<IBookObserver, BookObserver>();
        services.AddScoped<IRentedBookObserver, RentedBookObserver>();
        services.AddScoped<IVisitorObserver, VisitorObserver>();
        services.AddScoped<IViolationObserver, ViolationObserver>();
        services.AddScoped<ILoggerObserver, LoggerObserver>();
        return services;
    }
}
