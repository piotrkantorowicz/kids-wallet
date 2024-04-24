using Alba;

using KidsWallet.Persistence;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Npgsql;

using Weasel.Postgresql;

namespace KidsWallet.Tests.E2E;

[SetUpFixture]
public class WebApp
{
    private const string Environment = "E2E";
    
    public static IAlbaHost Host { get; private set; }
    
    [OneTimeSetUp]
    public async Task Init()
    {
        Host = await AlbaHost.For<Program>(x =>
        {
            x.UseEnvironment(Environment);
        });
    }
    
    [OneTimeTearDown]
    public async Task Teardown()
    {
        EfCoreSettings databaseSettings = GetDatabaseSettings();
        await DropDatabase(databaseSettings);
        Host.Dispose();
    }
    
    private static EfCoreSettings GetDatabaseSettings()
    {
        IConfiguration configuration = Host.Services.GetRequiredService<IConfiguration>();
        EfCoreSettings databaseSettings = new();
        configuration.Bind("EfCore", databaseSettings);
        
        return databaseSettings;
    }
    
    private static async Task DropDatabase(EfCoreSettings databaseSettings)
    {
        await using (NpgsqlConnection connection = new(databaseSettings.DefaultConnectionString))
        {
            await connection.OpenAsync();
            CommandBuilder dbCommandBuilder = new();
            dbCommandBuilder.Append($"DROP DATABASE IF EXISTS {databaseSettings.Database} WITH (FORCE);");
            await connection.ExecuteNonQueryAsync(dbCommandBuilder);
            await connection.CloseAsync();
        }
    }
}