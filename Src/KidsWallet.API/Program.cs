using KidsWallet.API.Configuration;
using KidsWallet.API.Configuration.Exceptions;
using KidsWallet.API.Endpoints.Crud;

// Create the builder and register the services
WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.AddKidsWallet();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

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
app.UseExceptionHandler();
await app.RunAsync();