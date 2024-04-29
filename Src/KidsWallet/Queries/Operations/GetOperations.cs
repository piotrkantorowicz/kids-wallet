using KidsWallet.API.Proxy.Requests.Operations.GET.GetKidAccountOperations.Request;
using KidsWallet.API.Proxy.Requests.Operations.GET.GetKidAccountOperations.Response;
using KidsWallet.Persistence.Model.Operations;
using KidsWallet.Persistence.Model.Operations.Specifications;
using KidsWallet.Services.Abstraction;

namespace KidsWallet.Queries.Operations;

public sealed record GetKidAccountOperationsQuery(
    Guid? Id = default,
    Guid? KidAccountId = default,
    string? Description = default,
    decimal? Amount = default,
    DateTime? DueDate = default,
    GetKidAccountOperationsRequest_OperationType? OperationType = default,
    DateTime? CreatedAt = default,
    DateTime? UpdatedAt = default);

public static class GetKidAccountOperationsQueryQueryHandler
{
    public static async Task<IReadOnlyCollection<GetKidAccountOperationsResponse>> Handle(
        GetKidAccountOperationsQuery query, ICrudOperationsService<KidAccountOperation> kidWalletCrudOperationsService,
        CancellationToken cancellationToken)
    {
        IReadOnlyCollection<KidAccountOperation> kidAccountOperations =
            await kidWalletCrudOperationsService.GetManyAsync(Map(query), cancellationToken);
        
        return Map(kidAccountOperations);
    }
    
    private static IReadOnlyCollection<GetKidAccountOperationsResponse> Map(
        IEnumerable<KidAccountOperation> kidAccountOperations)
    {
        return kidAccountOperations
            .Select(x => new GetKidAccountOperationsResponse(x.Id, x.KidAccountId, x.Description, Amount: x.Amount,
                DueDate: x.DueDate, OperationType: (GetKidAccountOperationsResponse_OperationType)x.OperationType))
            .ToList()
            .AsReadOnly();
    }
    
    private static KidAccountOperationSpecification Map(GetKidAccountOperationsQuery query)
    {
        return new KidAccountOperationSpecification(query.Id, query.KidAccountId, query.Description, query.Amount,
            query.DueDate, (OperationType?)query.OperationType, query.CreatedAt, query.UpdatedAt);
    }
}