using KidsWallet.Extensions;
using KidsWallet.Persistence;

namespace KidsWallet.API.Configuration;

public static class ServiceCollectionExtensions
{
    public static WebApplicationBuilder AddKidsWallet(this WebApplicationBuilder builder)
    {
        EfCoreSettings databaseSettings = new();
        builder.Configuration.Bind(key: "EfCore", instance: databaseSettings);
        builder.Services.AddKidsWallet(databaseSettings: databaseSettings);
        builder.Host.AddWolverine(databaseSettings: databaseSettings);

        return builder;
    }

    public static WebApplication CreateKidsWalletDatabase(this WebApplication app)
    {
        app.Services.MigrateDbContext();

        return app;
    }
}