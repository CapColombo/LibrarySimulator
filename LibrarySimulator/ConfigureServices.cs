using FluentValidation.AspNetCore;
using Library.DAL;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure;
using System.Security.Claims;
using System.Text;

namespace LibrarySimulator;

internal class ConfigureServices
{
    internal static void ConfigureAuth(IHostApplicationBuilder builder)
    {
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = builder.Configuration["Auth:Issuer"],
                        ValidAudience = builder.Configuration["Auth:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Auth:SignInKey"])),
                        RoleClaimType = ClaimTypes.Role
                    };
                });

        builder.Services.AddAuthorization();
    }

    internal static void ConfigureDatabase(IHostApplicationBuilder builder)
    {
        string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
        Action<NpgsqlDbContextOptionsBuilder> npgsqlOptionsAction;
        builder.Services.AddDbContext<LibraryContext>(o =>
        {
            npgsqlOptionsAction = cfg =>
            {
                cfg.MigrationsAssembly("Library.Migrations");
                cfg.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
                cfg.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
            };
            o.UseNpgsql(connectionString, npgsqlOptionsAction);
        });

        builder.Services.AddScoped<ILibraryContext, LibraryContext>();
    }

    internal static void ConfigureLogging(IHostApplicationBuilder builder)
    {
        builder.Services.AddLogging(cfg =>
        {
            cfg.ClearProviders();
            cfg.AddConfiguration(builder.Configuration.GetSection("Logging"));
#if DEBUG
            cfg.AddDebug();
#endif
            if (builder.Environment.IsDevelopment())
            {
                cfg.AddConsole();
            }
        });
    }

    internal static void AddFluentValidation(IHostApplicationBuilder builder)
    {
        builder.Services.AddFluentValidationAutoValidation()
                        .AddFluentValidationClientsideAdapters();
    }
}
