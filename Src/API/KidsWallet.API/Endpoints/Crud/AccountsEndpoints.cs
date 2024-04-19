using KidsWallet.Commands.Accounts;
using KidsWallet.Queries.Accounts;

using Microsoft.AspNetCore.Mvc;

using Wolverine;

namespace KidsWallet.API.Endpoints.Crud;

public static class AccountsEndpoints
{
    private const string Tag = "Accounts";
    
    public static WebApplication AddAccountEndpoints(this WebApplication app)
    {
        app
            .MapGet("/accounts/{id:guid}",
                ([FromRoute] Guid id, [FromQuery] Guid? kidWalletId, [FromQuery] string? name,
                    [FromQuery] decimal? balance, [FromQuery] DateTimeOffset? updatedAt,
                    [FromQuery] DateTimeOffset? createdAt, [FromQuery] bool? includeKidAccountOperations,
                    [FromServices] IMessageBus bus) => bus.InvokeAsync<GetKidAccountResponse>(new GetKidAccountQuery(id,
                    kidWalletId, name, balance, createdAt, updatedAt, includeKidAccountOperations)))
            .WithTags(Tag);
        
        app
            .MapGet("/accounts",
                ([FromQuery] Guid? id, [FromQuery] Guid? kidWalletId, [FromQuery] string? name,
                    [FromQuery] decimal? balance, [FromQuery] DateTimeOffset? updatedAt,
                    [FromQuery] DateTimeOffset? createdAt, [FromQuery] bool? includeKidAccountOperations,
                    [FromServices] IMessageBus bus) => bus.InvokeAsync<GetKidAccountsResponse>(
                    new GetKidAccountsQuery(KidWalletId: kidWalletId, Name: name, Balance: balance,
                        CreatedAt: createdAt, UpdatedAt: updatedAt,
                        IncludeKidAccountOperations: includeKidAccountOperations)))
            .WithTags(Tag);
        
        app
            .MapPost("/accounts",
                ([FromBody] CreateKidAccountRequest request, [FromServices] IMessageBus bus) => bus.InvokeAsync(
                    new CreateKidAccountCommand(KidWalletId: request.KidWalletId, AccountId: request.AccountId,
                        Name: request.Name, Balance: request.Balance)))
            .WithTags(Tag);
        
        app
            .MapPut("/accounts/{id:guid}",
                ([FromRoute] Guid id, [FromBody] UpdateKidAccountRequest request, [FromServices] IMessageBus bus) =>
                    bus.InvokeAsync(new UpdateKidAccountCommand(id, request.Name, request.Balance)))
            .WithTags(Tag);
        
        app
            .MapDelete("/accounts/{id:guid}",
                ([FromRoute] Guid id, [FromServices] IMessageBus bus) =>
                    bus.InvokeAsync(new DeleteKidAccountCommand(id)))
            .WithTags(Tag);
        
        return app;
    }
}