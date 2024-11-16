﻿using Library.BLL.Modules.Visitors.AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace Library.BLL;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddDependencies(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(VisitorProfile));
        return services;
    }
}