using KidsWallet.Persistence;
using KidsWallet.Services;
using KidsWallet.Services.Abstraction;
using KidsWallet.Shared;
using KidsWallet.Shared.Abstraction;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Wolverine;
using Wolverine.FluentValidation;

namespace KidsWallet.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddKidsWallet(this IServiceCollection services, EfCoreSettings databaseSettings)
    {
        services.AddScoped<IClock, UtcClock>();

        services.AddScoped(serviceType: typeof(ICrudOperationsService<>),
            implementationType: typeof(CrudOperationsService<>));

        services.AddDbContext<KidsWalletDbContext>(optionsAction: x =>
        {
            x.UseNpgsql(connectionString: databaseSettings.ConnectionString, npgsqlOptionsAction: options =>
            {
                options.MigrationsAssembly(assemblyName: typeof(KidsWalletDbContext).Assembly.FullName);

                options.EnableRetryOnFailure(maxRetryCount: 5, maxRetryDelay: TimeSpan.FromSeconds(value: 10),
                    errorCodesToAdd: null);
            });
        }, optionsLifetime: ServiceLifetime.Singleton);

        return services;
    }

    public static IHostBuilder AddWolverine(this IHostBuilder hostBuilder, EfCoreSettings databaseSettings)
    {
        hostBuilder.UseWolverine(overrides: opts =>
        {
            opts.UseFluentValidation();
            opts.ApplicationAssembly = typeof(ServiceCollectionExtensions).Assembly;
            opts.Policies.AutoApplyTransactions();
            opts.Durability.Mode = DurabilityMode.MediatorOnly;
        });

        return hostBuilder;
    }

    public static IServiceProvider MigrateDbContext(this IServiceProvider serviceProvider)
    {
        using IServiceScope scope = serviceProvider.CreateScope();
        KidsWalletDbContext dbContext = scope.ServiceProvider.GetRequiredService<KidsWalletDbContext>();
        dbContext.Database.Migrate();

        return serviceProvider;
    }
}