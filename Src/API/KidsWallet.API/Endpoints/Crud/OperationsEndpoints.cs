using KidsWallet.Commands.Operations;
using KidsWallet.Persistence.Model.Operations;
using KidsWallet.Queries.Operations;

using Microsoft.AspNetCore.Mvc;

using Wolverine;

namespace KidsWallet.API.Endpoints.Crud;

public static class OperationsEndpoints
{
    private const string Tag = "Operations";
    
    public static WebApplication AddOperationsEndpoints(this WebApplication app)
    {
        app
            .MapGet("/operations/{id:guid}",
                ([FromRoute] Guid id, [FromQuery] Guid? kidAccountId, [FromQuery] string? description,
                        [FromQuery] decimal? amount, [FromQuery] DateTimeOffset? dueDate,
                        [FromQuery] DateTimeOffset? updatedAt, [FromQuery] DateTimeOffset? createdAt,
                        [FromServices] IMessageBus bus) =>
                    bus.InvokeAsync<GetKidAccountOperationResponse>(new GetKidAccountOperationQuery(id, kidAccountId,
                        description, amount, dueDate, CreatedAt: createdAt, UpdatedAt: updatedAt)))
            .WithTags(Tag);
        
        app
            .MapGet("/operations",
                ([FromQuery] Guid? id, [FromQuery] Guid? kidAccountId, [FromQuery] string? description,
                    [FromQuery] decimal? amount, [FromQuery] DateTimeOffset? dueDate,
                    [FromQuery] DateTimeOffset? updatedAt, [FromQuery] DateTimeOffset? createdAt,
                    [FromServices] IMessageBus bus) => bus.InvokeAsync<GetKidAccountOperationsResponse>(
                    new GetKidAccountOperationsQuery(KidAccountId: kidAccountId, Description: description,
                        Amount: amount, DueDate: dueDate, CreatedAt: createdAt, UpdatedAt: updatedAt)))
            .WithTags(Tag);
        
        app
            .MapPost("/operations",
                ([FromBody] CreateKidAccountOperationRequest request, [FromServices] IMessageBus bus) =>
                    bus.InvokeAsync(new CreateKidAccountOperationCommand(request.KidAccountOperationId,
                        request.KidAccountId, DueDate: request.DueDate, Amount: request.Amount, Title: request.Title,
                        OperationType: (OperationType)request.OperationType)))
            .WithTags(Tag);
        
        app
            .MapPut("/operations/{id:guid}",
                ([FromRoute] Guid id, [FromBody] UpdateKidAccountOperationRequest request,
                    [FromServices] IMessageBus bus) => bus.InvokeAsync(new UpdateKidAccountOperationCommand(id,
                    DueDate: request.DueDate, Amount: request.Amount, Title: request.Title,
                    OperationType: request.OperationType)))
            .WithTags(Tag);
        
        app
            .MapDelete("/operations/{id:guid}",
                ([FromRoute] Guid id, [FromServices] IMessageBus bus) =>
                    bus.InvokeAsync(new DeleteKidAccountOperationCommand(id)))
            .WithTags(Tag);
        
        return app;
    }
}