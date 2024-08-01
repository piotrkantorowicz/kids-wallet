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
        string? environment = Environment.GetEnvironmentVariable(variable: "ASPNETCORE_ENVIRONMENT");
        string? startupProjectPath = args?[0];

        if (string.IsNullOrEmpty(value: startupProjectPath) || !Path.Exists(path: startupProjectPath))
        {
            string errorMessage =
                $"Startup project path parameter must be provided and must exists on current machine. Actual value: {startupProjectPath}";

            throw new InvalidOperationException(message: errorMessage);
        }

        Console.WriteLine(format: "Startup project path: {0}", arg0: startupProjectPath);

        IConfiguration configuration = new ConfigurationBuilder()
            .SetBasePath(basePath: startupProjectPath)
            .AddJsonFile(path: $"{SettingsFileName}.json", optional: false, reloadOnChange: true)
            .AddJsonFile(path: $"{SettingsFileName}.{environment}.json", optional: true)
            .AddEnvironmentVariables()
            .Build();

        EfCoreSettings databaseSettings = new();
        configuration.Bind(key: "EfCore", instance: databaseSettings);
        optionsBuilder.UseNpgsql(connectionString: databaseSettings.ConnectionString);

        return new KidsWalletDbContext(options: optionsBuilder.Options);
    }
}