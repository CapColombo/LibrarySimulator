using Library.BLL.Modules.Admin.AutoMapper;
using Library.BLL.Modules.Books.AutoMapper;
using Library.BLL.Modules.Operations.AutoMapper;
using Library.BLL.Modules.Visitors.AutoMapper;
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
        return services;
    }

    private static IServiceCollection AddAutoMapperDependencies(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(OperationProfile));
        services.AddAutoMapper(typeof(BookProfile));
        services.AddAutoMapper(typeof(VisitorProfile));
        services.AddAutoMapper(typeof(EmployeeProfile));
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
