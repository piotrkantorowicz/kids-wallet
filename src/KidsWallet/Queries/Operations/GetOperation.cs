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
        KidAccountOperation? kidAccountOperation = await kidWalletCrudOperationsService.GetAsync(
            specification: Map(query: query), throwWhenNotFound: true, cancellationToken: cancellationToken);

        return Map(kidAccountOperation: kidAccountOperation);
    }

    private static GetKidAccountOperationResponse? Map(KidAccountOperation? kidAccountOperation)
    {
        if (kidAccountOperation is null)
        {
            return null;
        }

        return new GetKidAccountOperationResponse(KidAccountOperationId: kidAccountOperation.Id,
            KidAccountId: kidAccountOperation.KidAccountId, Description: kidAccountOperation.Description,
            Amount: kidAccountOperation.Amount, DueDate: kidAccountOperation.DueDate,
            OperationType: (GetKidAccountOperationResponse_OperationType)kidAccountOperation.OperationType);
    }

    private static KidAccountOperationSpecification Map(GetKidAccountOperationQuery query)
    {
        return new KidAccountOperationSpecification(Id: query.KidAccountOperationId, KidAccountId: query.KidAccountId,
            Description: query.Description, Amount: query.Amount, DueDate: query.DueDate,
            OperationType: (OperationType?)query.OperationType, CreatedAt: query.CreatedAt, UpdatedAt: query.UpdatedAt);
    }
}