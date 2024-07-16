using KidsWallet.API.Configuration;
using KidsWallet.API.Configuration.Exceptions;

// Create the builder and register the services
WebApplicationBuilder builder = WebApplication.CreateBuilder(args: args);
builder.AddKidsWallet();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddProblemDetails();
builder.Services.AddControllers(configure: options => options.Filters.Add<GlobalExceptionFilter>());

// Build and configure the app
WebApplication app = builder.Build();
app.CreateKidsWalletDatabase();

if (app.Environment.IsProduction() is false)
{
    app.UseSwaggerUI();
    app.UseSwagger();
}

app.MapGet(pattern: "/", handler: () => "Hello World!").WithTags("Hello");
app.MapControllers();
app.UseExceptionHandler();
await app.RunAsync();