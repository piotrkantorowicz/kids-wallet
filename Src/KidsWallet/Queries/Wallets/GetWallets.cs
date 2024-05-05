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
            await kidWalletCrudOperationsService.GetManyAsync(Map(query), cancellationToken);
        
        return Map(kidWallets);
    }
    
    private static IReadOnlyCollection<GetKidWalletsResponse> Map(IEnumerable<KidWallet> kidWallets)
    {
        return kidWallets
            .Select(x => new GetKidWalletsResponse(x.Id, x.KidId, x.Name,
                x
                    .KidAccounts.Select(y => new GetKidWalletsResponse_KidAccount(y.Id, y.KidWalletId, y.Name,
                        y.Balance,
                        y
                            .KidAccountOperations.Select(z => new GetKidWalletsResponse_KidAccountOperation(z.Id,
                                z.KidAccountId, z.Description, z.DueDate, z.Amount,
                                (GetKidWalletsResponse_OperationType)z.OperationType))
                            .ToList()))
                    .ToList()))
            .ToList();
    }
    
    private static KidWalletSpecification Map(GetKidWalletsQuery query)
    {
        return new KidWalletSpecification(query.KidWalletId, query.KidId, query.Name, query.CreatedAt, query.UpdatedAt,
            query.IncludeKidAccounts, query.IncludeKidAccountOperations);
    }
}