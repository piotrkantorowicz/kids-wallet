﻿using KidsWallet.Commands.Wallets;
using KidsWallet.Queries.Wallets;

using Microsoft.AspNetCore.Mvc;

using Wolverine;

namespace KidsWallet.API.Endpoints.Crud;

public static class WalletsEndpoints
{
    private const string Tag = "Wallets";
    
    public static WebApplication AddWalletEndpoints(this WebApplication app)
    {
        app
            .MapGet("/wallets/{id:guid}", ([FromRoute] Guid id, [FromQuery] Guid? kidId, [FromQuery] string? name,
                [FromQuery] DateTime? createdAt, [FromQuery] DateTime? updatedAt, [FromQuery] bool? includeKidAccounts,
                [FromQuery] bool? includeKidAccountOperations, [FromServices] IMessageBus bus) =>
            {
                Task<GetKidWalletResponse> response = bus.InvokeAsync<GetKidWalletResponse>(new GetKidWalletQuery(id,
                    kidId,
                    name, createdAt, updatedAt, includeKidAccounts, includeKidAccountOperations));
                
                return response;
            })
            .WithTags(Tag);
        
        app
            .MapGet("/wallets", ([FromQuery] Guid? id, [FromQuery] Guid? kidId, [FromQuery] string? name,
                [FromQuery] DateTime? createdAt, [FromQuery] DateTime? updatedAt, [FromQuery] bool? includeKidAccounts,
                [FromQuery] bool? includeKidAccountOperations, [FromServices] IMessageBus bus) =>
            {
                Task<GetKidWalletsResponse> response = bus.InvokeAsync<GetKidWalletsResponse>(new GetKidWalletsQuery(id,
                    kidId, name, createdAt, updatedAt, includeKidAccounts, includeKidAccountOperations));
                
                return response;
            })
            .WithTags(Tag);
        
        app
            .MapPost("/wallets",
                ([FromBody] CreateKidWalletRequest request, [FromServices] IMessageBus bus) => bus.InvokeAsync(
                    new CreateKidWalletCommand(request.WalletId, request.KidId, request.Name)))
            .WithTags(Tag);
        
        app
            .MapPut("/wallets/{id:guid}",
                ([FromRoute] Guid id, [FromBody] UpdateKidWalletRequest request, [FromServices] IMessageBus bus) =>
                    bus.InvokeAsync(new UpdateKidWalletCommand(id, request.Name)))
            .WithTags(Tag);
        
        app
            .MapDelete("/wallets/{id:guid}",
                ([FromRoute] Guid id, [FromServices] IMessageBus bus) =>
                    bus.InvokeAsync(new DeleteKidWalletCommand(id)))
            .WithTags(Tag);
        
        return app;
    }
}