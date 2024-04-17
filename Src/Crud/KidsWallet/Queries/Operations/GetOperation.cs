using KidsWallet.Persistence.Model.Operations;
using KidsWallet.Persistence.Model.Operations.Specifications;
using KidsWallet.Services.Abstraction;

namespace KidsWallet.Queries.Operations;

public enum GetKidAccountOperationRequest_OperationType
{
    None,
    Income,
    Expense,
    Transfer
}

public sealed record GetKidAccountOperationResponse(
    Guid Id,
    Guid KidAccountId,
    string? Description,
    DateTimeOffset DueDate,
    decimal Amount,
    GetKidAccountOperationResponse_OperationType OperationType);

public enum GetKidAccountOperationResponse_OperationType
{
    None,
    Income,
    Expense,
    Transfer
}

public sealed record GetKidAccountOperationQuery(
    Guid? Id = default,
    Guid? KidAccountId = default,
    string? Description = default,
    decimal? Amount = default,
    DateTimeOffset? DueDate = default,
    GetKidAccountOperationRequest_OperationType? OperationType = default,
    DateTimeOffset? CreatedAt = default,
    DateTimeOffset? UpdatedAt = default);

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
        return new KidAccountOperationSpecification(query.Id, query.KidAccountId, query.Description, query.Amount,
            query.DueDate, (OperationType?)query.OperationType, query.CreatedAt, query.UpdatedAt);
    }
}