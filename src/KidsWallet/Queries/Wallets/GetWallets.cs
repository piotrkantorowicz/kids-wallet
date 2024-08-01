using KidsWallet.API.Proxy.Requests.Wallets.GET.GetKidWallets.Response;
using KidsWallet.Persistence.Model.Wallets;
using KidsWallet.Persistence.Model.Wallets.Specifications;
using KidsWallet.Services.Abstraction;

namespace KidsWallet.Queries.Wallets;

public sealed record GetKidWalletsQuery(
    Guid? KidWalletId = default,
    Guid? KidId = default,
    string? Name = default,
    DateTime? CreatedAt = default,
    DateTime? UpdatedAt = default,
    bool? IncludeKidAccounts = default,
    bool? IncludeKidAccountOperations = default);

public class GetWalletsQueryHandler
{
    public static async Task<IReadOnlyCollection<GetKidWalletsResponse>> Handle(GetKidWalletsQuery query,
        ICrudOperationsService<KidWallet> kidWalletCrudOperationsService, CancellationToken cancellationToken)
    {
        IReadOnlyCollection<KidWallet> kidWallets =
            await kidWalletCrudOperationsService.GetManyAsync(specification: Map(query: query),
                cancellationToken: cancellationToken);

        return Map(kidWallets: kidWallets);
    }

    private static IReadOnlyCollection<GetKidWalletsResponse> Map(IEnumerable<KidWallet> kidWallets)
    {
        return kidWallets
            .Select(selector: x => new GetKidWalletsResponse(KidWalletId: x.Id, KidId: x.KidId, Name: x.Name,
                KidAccounts: x
                    .KidAccounts.Select(selector: y => new GetKidWalletsResponse_KidAccount(KidAccountId: y.Id,
                        KidWalletId: y.KidWalletId, Name: y.Name, Balance: y.Balance,
                        Operations: y
                            .KidAccountOperations.Select(selector: z =>
                                new GetKidWalletsResponse_KidAccountOperation(KidAccountOperationId: z.Id,
                                    KidAccountId: z.KidAccountId, Description: z.Description, DueDate: z.DueDate,
                                    Amount: z.Amount,
                                    OperationType: (GetKidWalletsResponse_OperationType)z.OperationType))
                            .ToList()))
                    .ToList()))
            .ToList();
    }

    private static KidWalletSpecification Map(GetKidWalletsQuery query)
    {
        return new KidWalletSpecification(Id: query.KidWalletId, KidId: query.KidId, Name: query.Name,
            CreatedAt: query.CreatedAt, UpdatedAt: query.UpdatedAt, IncludeKidAccounts: query.IncludeKidAccounts,
            IncludeKidAccountOperations: query.IncludeKidAccountOperations);
    }
}