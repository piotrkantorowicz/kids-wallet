using KidsWallet.API.Proxy.Requests.Accounts.GET.GetKidAccount.Response;
using KidsWallet.Persistence.Model.Accounts;
using KidsWallet.Persistence.Model.Accounts.Specifications;
using KidsWallet.Services.Abstraction;

namespace KidsWallet.Queries.Accounts;

public sealed record GetKidAccountQuery(
    Guid? Id = default,
    Guid? KidWalletId = default,
    string? Name = default,
    decimal? Balance = default,
    DateTime? CreatedAt = default,
    DateTime? UpdatedAt = default,
    bool? IncludeKidAccountOperations = default);

public static class GetKidAccountQueryQueryHandler
{
    public static async Task<GetKidAccountResponse?> Handle(GetKidAccountQuery query,
        ICrudOperationsService<KidAccount> kidWalletCrudOperationsService, CancellationToken cancellationToken)
    {
        KidAccount? kidAccount = await kidWalletCrudOperationsService.GetAsync(Map(query), true, cancellationToken);
        
        return Map(kidAccount);
    }
    
    private static GetKidAccountResponse? Map(KidAccount? kidAccount)
    {
        if (kidAccount is null)
        {
            return null;
        }
        
        return new GetKidAccountResponse(kidAccount.Id, kidAccount.KidWalletId, kidAccount.Name, kidAccount.Balance,
            kidAccount
                .KidAccountOperations.Select(x => new GetKidAccountResponse_KidAccountOperation(x.Id, x.KidAccountId,
                    x.Description, x.DueDate, x.Amount, (GetKidAccountResponse_OperationType)x.OperationType))
                .ToList());
    }
    
    private static KidAccountSpecification Map(GetKidAccountQuery query)
    {
        return new KidAccountSpecification(query.Id, query.KidWalletId, query.Name, query.Balance, query.CreatedAt,
            query.UpdatedAt, query.IncludeKidAccountOperations);
    }
}