using KidsWallet.API.Configuration.Modules.Crud;
using KidsWallet.API.Configuration.Settings;
using KidsWallet.API.Endpoints.Crud;

using Oakton;

// Create the builder and register the services
WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
ApplicationSettings applicationSettings = new();
builder.Configuration.Bind("App", applicationSettings);

switch (applicationSettings.Type)
{
    case ApplicationType.Crud:
        builder.AddKidsWalletCrud();
        
        break;
    default:
        throw new ArgumentOutOfRangeException();
}

builder.Services.AddProblemDetails();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Build and configure the app
WebApplication app = builder.Build();

switch (applicationSettings.Type)
{
    case ApplicationType.Crud:
        app.CreateKidsWalletCrudDatabase();
        
        break;
    default:
        throw new ArgumentOutOfRangeException();
}

if (app.Environment.IsProduction() is false)
{
    app.UseSwaggerUI();
    app.UseSwagger();
}

app.MapGet("/", () => "Hello World!").WithTags("Hello");
app.AddWalletEndpoints();
app.AddAccountEndpoints();
app.AddOperationsEndpoints();

return await app.RunOaktonCommands(args);