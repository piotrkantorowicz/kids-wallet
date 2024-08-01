using KidsWallet.API.Proxy.Requests.Operations.GET.GetKidAccountOperations.Request;
using KidsWallet.API.Proxy.Requests.Operations.GET.GetKidAccountOperations.Response;
using KidsWallet.Persistence.Model.Operations;
using KidsWallet.Persistence.Model.Operations.Specifications;
using KidsWallet.Services.Abstraction;

namespace KidsWallet.Queries.Operations;

public sealed record GetKidAccountOperationsQuery(
    Guid? KidAccountOperationId = default,
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
            await kidWalletCrudOperationsService.GetManyAsync(specification: Map(query: query),
                cancellationToken: cancellationToken);

        return Map(kidAccountOperations: kidAccountOperations);
    }

    private static IReadOnlyCollection<GetKidAccountOperationsResponse> Map(
        IEnumerable<KidAccountOperation> kidAccountOperations)
    {
        return kidAccountOperations
            .Select(selector: x => new GetKidAccountOperationsResponse(KidAccountOperationId: x.Id,
                KidAccountId: x.KidAccountId, Description: x.Description, Amount: x.Amount, DueDate: x.DueDate,
                OperationType: (GetKidAccountOperationsResponse_OperationType)x.OperationType))
            .ToList()
            .AsReadOnly();
    }

    private static KidAccountOperationSpecification Map(GetKidAccountOperationsQuery query)
    {
        return new KidAccountOperationSpecification(Id: query.KidAccountOperationId, KidAccountId: query.KidAccountId,
            Description: query.Description, Amount: query.Amount, DueDate: query.DueDate,
            OperationType: (OperationType?)query.OperationType, CreatedAt: query.CreatedAt, UpdatedAt: query.UpdatedAt);
    }
}