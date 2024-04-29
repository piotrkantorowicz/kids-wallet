using KidsWallet.API.Proxy.Requests.Operations.GET.GetKidAccountOperation.Response;
using KidsWallet.API.Proxy.Requests.Operations.GET.GetKidAccountOperations.Response;
using KidsWallet.API.Proxy.Requests.Operations.POST.CreateKidAccountOperation.Request;
using KidsWallet.API.Proxy.Requests.Operations.PUT.UpdateKidAccountOperation.Request;
using KidsWallet.Commands.Operations;
using KidsWallet.Persistence.Model.Operations;
using KidsWallet.Queries.Operations;

using Microsoft.AspNetCore.Mvc;

using Wolverine;

namespace KidsWallet.API.Endpoints;

public static class OperationsEndpoints
{
    private const string Tag = "Operations";
    
    public static WebApplication AddOperationsEndpoints(this WebApplication app)
    {
        app
            .MapGet("/v1/operations/{id:guid}",
                ([FromRoute] Guid id, [FromQuery] Guid? kidAccountId, [FromQuery] string? description,
                        [FromQuery] decimal? amount, [FromQuery] DateTime? dueDate, [FromQuery] DateTime? updatedAt,
                        [FromQuery] DateTime? createdAt, [FromServices] IMessageBus bus) =>
                    bus.InvokeAsync<GetKidAccountOperationResponse>(new GetKidAccountOperationQuery(id, kidAccountId,
                        description, amount, dueDate, CreatedAt: createdAt, UpdatedAt: updatedAt)))
            .WithTags(Tag);
        
        app
            .MapGet("/v1/operations",
                ([FromQuery] Guid? id, [FromQuery] Guid? kidAccountId, [FromQuery] string? description,
                        [FromQuery] decimal? amount, [FromQuery] DateTime? dueDate, [FromQuery] DateTime? updatedAt,
                        [FromQuery] DateTime? createdAt, [FromServices] IMessageBus bus) =>
                    bus.InvokeAsync<IReadOnlyCollection<GetKidAccountOperationsResponse>>(
                        new GetKidAccountOperationsQuery(
                            KidAccountId: kidAccountId, Description: description, Amount: amount, DueDate: dueDate,
                            CreatedAt: createdAt, UpdatedAt: updatedAt)))
            .WithTags(Tag);
        
        app
            .MapPost("/v1/operations",
                ([FromBody] CreateKidAccountOperationRequest request, [FromServices] IMessageBus bus) =>
                    bus.InvokeAsync(new CreateKidAccountOperationCommand(request.OperationId, request.AccountId,
                        DueDate: request.DueDate, Amount: request.Amount, Title: request.Title,
                        OperationType: (OperationType)request.OperationType)))
            .WithTags(Tag);
        
        app
            .MapPut("/v1/operations/{id:guid}",
                ([FromRoute] Guid id, [FromBody] UpdateKidAccountOperationRequest request,
                    [FromServices] IMessageBus bus) => bus.InvokeAsync(new UpdateKidAccountOperationCommand(id,
                    DueDate: request.DueDate, Amount: request.Amount, Title: request.Title,
                    OperationType: (OperationType)request.OperationType)))
            .WithTags(Tag);
        
        app
            .MapDelete("/v1/operations/{id:guid}",
                ([FromRoute] Guid id, [FromServices] IMessageBus bus) =>
                    bus.InvokeAsync(new DeleteKidAccountOperationCommand(id)))
            .WithTags(Tag);
        
        return app;
    }
}