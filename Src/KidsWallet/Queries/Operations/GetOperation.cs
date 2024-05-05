using KidsWallet.API.Proxy.Requests.Operations.GET.GetKidAccountOperation.Request;
using KidsWallet.API.Proxy.Requests.Operations.GET.GetKidAccountOperation.Response;
using KidsWallet.Persistence.Model.Operations;
using KidsWallet.Persistence.Model.Operations.Specifications;
using KidsWallet.Services.Abstraction;

namespace KidsWallet.Queries.Operations;

public sealed record GetKidAccountOperationQuery(
    Guid? KidAccountOperationId = default,
    Guid? KidAccountId = default,
    string? Description = default,
    decimal? Amount = default,
    DateTime? DueDate = default,
    GetKidAccountOperationRequest_OperationType? OperationType = default,
    DateTime? CreatedAt = default,
    DateTime? UpdatedAt = default);

public static class GetKidAccountOperationQueryQueryHandler
{
    public static async Task<GetKidAccountOperationResponse?> Handle(GetKidAccountOperationQuery query,
        ICrudOperationsService<KidAccountOperation> kidWalletCrudOperationsService, CancellationToken cancellationToken)
    {
        KidAccountOperation? kidAccountOperation =
            await kidWalletCrudOperationsService.GetAsync(Map(query), true, cancellationToken);
        
        return Map(kidAccountOperation);
    }
    
    private static GetKidAccountOperationResponse? Map(KidAccountOperation? kidAccountOperation)
    {
        if (kidAccountOperation is null)
        {
            return null;
        }
        
        return new GetKidAccountOperationResponse(kidAccountOperation.Id, kidAccountOperation.KidAccountId,
            kidAccountOperation.Description, Amount: kidAccountOperation.Amount, DueDate: kidAccountOperation.DueDate,
            OperationType: (GetKidAccountOperationResponse_OperationType)kidAccountOperation.OperationType);
    }
    
    private static KidAccountOperationSpecification Map(GetKidAccountOperationQuery query)
    {
        return new KidAccountOperationSpecification(query.KidAccountOperationId, query.KidAccountId, query.Description,
            query.Amount, query.DueDate, (OperationType?)query.OperationType, query.CreatedAt, query.UpdatedAt);
    }
}