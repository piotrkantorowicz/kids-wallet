using KidsWallet.Extensions;
using KidsWallet.Persistence;

namespace KidsWallet.API.Configuration;

public static class ServiceCollectionExtensions
{
    public static WebApplicationBuilder AddKidsWallet(this WebApplicationBuilder builder)
    {
        EfCoreSettings databaseSettings = new();
        builder.Configuration.Bind("EfCore", databaseSettings);
        builder.Services.AddKidsWallet(databaseSettings);
        builder.Host.AddWolverine(databaseSettings);
        
        return builder;
    }
    
    public static WebApplication CreateKidsWalletDatabase(this WebApplication app)
    {
        app.Services.MigrateDbContext();
        
        return app;
    }
}