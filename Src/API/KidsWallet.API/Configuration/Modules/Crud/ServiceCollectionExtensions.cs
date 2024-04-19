using KidsWallet.Extensions;
using KidsWallet.Persistence;

using Oakton;

namespace KidsWallet.API.Configuration.Modules.Crud;

public static class ServiceCollectionExtensions
{
    public static WebApplicationBuilder AddKidsWalletCrud(this WebApplicationBuilder builder)
    {
        EfCoreSettings databaseSettings = new();
        builder.Configuration.Bind("EfCore", databaseSettings);
        builder.Services.AddKidsWallet(databaseSettings);
        builder.Host.AddWolverine(databaseSettings);
        builder.Host.ApplyOaktonExtensions();
        
        return builder;
    }
    
    public static WebApplication CreateKidsWalletCrudDatabase(this WebApplication app)
    {
        app.Services.MigrateDbContext();
        
        return app;
    }
}