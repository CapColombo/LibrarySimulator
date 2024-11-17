using Library.BLL.Modules.Admin.AutoMapper;
using Library.BLL.Modules.Books.AutoMapper;
using Library.BLL.Modules.Visitors.AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace Library.BLL;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddDependencies(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(BookProfile));
        services.AddAutoMapper(typeof(VisitorProfile));
        services.AddAutoMapper(typeof(EmployeeProfile));
        return services;
    }
}
