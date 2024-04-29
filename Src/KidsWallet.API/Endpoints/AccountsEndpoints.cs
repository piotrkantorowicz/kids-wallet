﻿using KidsWallet.API.Proxy.Requests.Accounts.GET.GetKidAccount.Response;
using KidsWallet.API.Proxy.Requests.Accounts.GET.GetKidAccounts.Response;
using KidsWallet.API.Proxy.Requests.Accounts.POST.CreateKidAccount.Request;
using KidsWallet.API.Proxy.Requests.Accounts.PUT.UpdateKidAccounts.Request;
using KidsWallet.Commands.Accounts;
using KidsWallet.Queries.Accounts;

using Microsoft.AspNetCore.Mvc;

using Wolverine;

namespace KidsWallet.API.Endpoints;

public static class AccountsEndpoints
{
    private const string Tag = "Accounts";
    
    public static WebApplication AddAccountEndpoints(this WebApplication app)
    {
        app
            .MapGet("/v1/accounts/{id:guid}",
                ([FromRoute] Guid id, [FromQuery] Guid? kidWalletId, [FromQuery] string? name,
                        [FromQuery] decimal? balance, [FromQuery] DateTime? updatedAt, [FromQuery] DateTime? createdAt,
                        [FromQuery] bool? includeKidAccountOperations, [FromServices] IMessageBus bus) =>
                    bus.InvokeAsync<GetKidAccountResponse>(new GetKidAccountQuery(id, kidWalletId, name, balance,
                        createdAt,
                        updatedAt, includeKidAccountOperations)))
            .WithTags(Tag);
        
        app
            .MapGet("/v1/accounts",
                ([FromQuery] Guid? id, [FromQuery] Guid? kidWalletId, [FromQuery] string? name,
                        [FromQuery] decimal? balance, [FromQuery] DateTime? updatedAt, [FromQuery] DateTime? createdAt,
                        [FromQuery] bool? includeKidAccountOperations, [FromServices] IMessageBus bus) =>
                    bus.InvokeAsync<IReadOnlyCollection<GetKidAccountsResponse>>(new GetKidAccountsQuery(KidWalletId: kidWalletId,
                        Name: name,
                        Balance: balance, CreatedAt: createdAt, UpdatedAt: updatedAt,
                        IncludeKidAccountOperations: includeKidAccountOperations)))
            .WithTags(Tag);
        
        app
            .MapPost("/v1/accounts",
                ([FromBody] CreateKidAccountRequest request, [FromServices] IMessageBus bus) => bus.InvokeAsync(
                    new CreateKidAccountCommand(WalletId: request.WalletId, AccountId: request.AccountId,
                        Name: request.Name, Balance: request.Balance)))
            .WithTags(Tag);
        
        app
            .MapPut("/v1/accounts/{id:guid}",
                ([FromRoute] Guid id, [FromBody] UpdateKidAccountRequest request, [FromServices] IMessageBus bus) =>
                    bus.InvokeAsync(new UpdateKidAccountCommand(id, request.Name, request.Balance)))
            .WithTags(Tag);
        
        app
            .MapDelete("/v1/accounts/{id:guid}",
                ([FromRoute] Guid id, [FromServices] IMessageBus bus) =>
                    bus.InvokeAsync(new DeleteKidAccountCommand(id)))
            .WithTags(Tag);
        
        return app;
    }
}