using KidsWallet.API.Endpoints;
using KidsWallet.Extensions;
using KidsWallet.Persistence;

using Oakton;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
EfCoreSettings databaseSettings = new();
builder.Configuration.Bind("EfCore", databaseSettings);
builder.Services.AddKidsWallet(databaseSettings);
builder.Services.AddProblemDetails();
builder.Host.AddWolverine(databaseSettings);
builder.Host.ApplyOaktonExtensions();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
WebApplication app = builder.Build();
app.Services.MigrateDbContext();
app.UseSwagger();
app.UseSwaggerUI();
app.MapGet("/", () => "Hello World!").WithTags("Hello");
app.AddWalletEndpoints();
app.AddAccountEndpoints();
app.AddOperationsEndpoints();

return await app.RunOaktonCommands(args);