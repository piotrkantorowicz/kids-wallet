using KidsWallet.API.Configuration.Modules.Crud;
using KidsWallet.API.Endpoints.Crud;

// Create the builder and register the services
WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.AddKidsWallet();
builder.Services.AddProblemDetails();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Build and configure the app
WebApplication app = builder.Build();
app.CreateKidsWalletDatabase();

if (app.Environment.IsProduction() is false)
{
    app.UseSwaggerUI();
    app.UseSwagger();
}

app.MapGet("/", () => "Hello World!").WithTags("Hello");
app.AddWalletEndpoints();
app.AddAccountEndpoints();
app.AddOperationsEndpoints();
await app.RunAsync();