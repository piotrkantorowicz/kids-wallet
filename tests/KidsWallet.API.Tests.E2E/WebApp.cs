using Alba;

using JasperFx.Core;

using KidsWallet.Persistence;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Npgsql;

using NUnit.Framework;

using Weasel.Postgresql;

namespace KidsWallet.API.Tests.E2E;

[SetUpFixture]
public class WebApp
{
    private const string Environment = "E2E";

    public static IAlbaHost Host { get; private set; } = null!;

    [OneTimeSetUp]
    public async Task Init()
    {
        Host = await AlbaHost.For<Program>(configuration: x =>
        {
            x.UseEnvironment(environment: Environment);
        });
    }

    [OneTimeTearDown]
    public async Task Teardown()
    {
        EfCoreSettings databaseSettings = GetDatabaseSettings();
        await DropDatabase(databaseSettings: databaseSettings);
        Host.SafeDispose();
    }

    private static EfCoreSettings GetDatabaseSettings()
    {
        IConfiguration configuration = Host.Services.GetRequiredService<IConfiguration>();
        EfCoreSettings databaseSettings = new();
        configuration.Bind(key: "EfCore", instance: databaseSettings);

        return databaseSettings;
    }

    private static async Task DropDatabase(EfCoreSettings databaseSettings)
    {
        await using (NpgsqlConnection connection = new(connectionString: databaseSettings.DefaultConnectionString))
        {
            await connection.OpenAsync();
            CommandBuilder dbCommandBuilder = new();
            dbCommandBuilder.Append(text: $"DROP DATABASE IF EXISTS {databaseSettings.Database} WITH (FORCE);");
            await connection.ExecuteNonQueryAsync(commandBuilder: dbCommandBuilder);
            await connection.CloseAsync();
        }
    }
}