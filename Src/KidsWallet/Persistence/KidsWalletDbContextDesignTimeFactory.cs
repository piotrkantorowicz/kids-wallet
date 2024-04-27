using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace KidsWallet.Persistence;

public sealed class NpsqlDbContextFactory : IDesignTimeDbContextFactory<KidsWalletDbContext>
{
    private const string SettingsFileName = "appsettings";
    
    public KidsWalletDbContext CreateDbContext(string[]? args)
    {
        DbContextOptionsBuilder<KidsWalletDbContext> optionsBuilder = new();
        string? environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        string? startupProjectPath = args?[0];
        
        if (string.IsNullOrEmpty(startupProjectPath) || !Path.Exists(startupProjectPath))
        {
            string errorMessage =
                $"Startup project path parameter must be provided and must exists on current machine. Actual value: {startupProjectPath}";
            
            throw new InvalidOperationException(errorMessage);
        }
        
        Console.WriteLine("Startup project path: {0}", startupProjectPath);
        
        IConfiguration configuration = new ConfigurationBuilder()
            .SetBasePath(startupProjectPath)
            .AddJsonFile($"{SettingsFileName}.json", false, true)
            .AddJsonFile($"{SettingsFileName}.{environment}.json", true)
            .AddEnvironmentVariables()
            .Build();
        
        EfCoreSettings databaseSettings = new();
        configuration.Bind("EfCore", databaseSettings);
        optionsBuilder.UseNpgsql(databaseSettings.ConnectionString);
        
        return new KidsWalletDbContext(optionsBuilder.Options);
    }
}