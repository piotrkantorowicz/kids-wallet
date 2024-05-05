using KidsWallet.API.Proxy.Requests.Operations.GET.GetKidAccountOperation.Response;
using KidsWallet.API.Proxy.Requests.Operations.GET.GetKidAccountOperations.Response;
using KidsWallet.API.Proxy.Requests.Operations.POST.CreateKidAccountOperation.Request;
using KidsWallet.API.Proxy.Requests.Operations.PUT.UpdateKidAccountOperation.Request;
using KidsWallet.Commands.Operations;
using KidsWallet.Persistence.Model.Operations;
using KidsWallet.Queries.Operations;

using Microsoft.AspNetCore.Mvc;

using Wolverine;

namespace KidsWallet.API.Controllers;

[ApiController, Route("v1/operations")]
public class OperationsController : ControllerBase
{
    private readonly IMessageBus _messageBus;
    
    public OperationsController(IMessageBus messageBus)
    {
        _messageBus = messageBus ?? throw new ArgumentNullException(nameof(messageBus));
    }
    
    [HttpGet("{id:guid}")]
    public async Task<GetKidAccountOperationResponse> GetOperation(Guid id, Guid? kidAccountId, string? description,
        decimal? amount, DateTime? dueDate, DateTime? updatedAt, DateTime? createdAt)
    {
        return await _messageBus.InvokeAsync<GetKidAccountOperationResponse>(new GetKidAccountOperationQuery(id,
            kidAccountId, description, amount, dueDate, CreatedAt: createdAt, UpdatedAt: updatedAt));
    }
    
    [HttpGet]
    public async Task<IReadOnlyCollection<GetKidAccountOperationsResponse>> GetOperations(Guid? id, Guid? kidAccountId,
        string? description, decimal? amount, DateTime? dueDate, DateTime? updatedAt, DateTime? createdAt)
    {
        return await _messageBus.InvokeAsync<IReadOnlyCollection<GetKidAccountOperationsResponse>>(
            new GetKidAccountOperationsQuery(id, kidAccountId, description, amount, dueDate, CreatedAt: createdAt,
                UpdatedAt: updatedAt));
    }
    
    [HttpPost]
    public async Task PostOperation([FromBody] CreateKidAccountOperationRequest request)
    {
        await _messageBus.InvokeAsync(new CreateKidAccountOperationCommand(request.KidAccountOperationId,
            request.KidAccountId, DueDate: request.DueDate, Amount: request.Amount, Title: request.Title,
            OperationType: (OperationType)request.OperationType));
    }
    
    [HttpPut("{id:guid}")]
    public async Task PutOperation(Guid id, [FromBody] UpdateKidAccountOperationRequest request)
    {
        await _messageBus.InvokeAsync(new UpdateKidAccountOperationCommand(id, DueDate: request.DueDate,
            Amount: request.Amount, Title: request.Title, OperationType: (OperationType)request.OperationType));
    }
    
    [HttpDelete("{id:guid}")]
    public async Task DeleteOperation(Guid id)
    {
        await _messageBus.InvokeAsync(new DeleteKidAccountOperationCommand(id));
    }
}