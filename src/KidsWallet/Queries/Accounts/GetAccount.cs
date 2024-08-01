using KidsWallet.API.Proxy.Requests.Accounts.GET.GetKidAccount.Response;
using KidsWallet.Persistence.Model.Accounts;
using KidsWallet.Persistence.Model.Accounts.Specifications;
using KidsWallet.Services.Abstraction;

namespace KidsWallet.Queries.Accounts;

public sealed record GetKidAccountQuery(
    Guid? KidAccountId = default,
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
        KidAccount? kidAccount = await kidWalletCrudOperationsService.GetAsync(specification: Map(query: query),
            throwWhenNotFound: true, cancellationToken: cancellationToken);

        return Map(kidAccount: kidAccount);
    }

    private static GetKidAccountResponse? Map(KidAccount? kidAccount)
    {
        if (kidAccount is null)
        {
            return null;
        }

        return new GetKidAccountResponse(KidAccountId: kidAccount.Id, KidWalletId: kidAccount.KidWalletId,
            Name: kidAccount.Name, Balance: kidAccount.Balance,
            Operations: kidAccount
                .KidAccountOperations.Select(selector: x => new GetKidAccountResponse_KidAccountOperation(
                    KidAccountOperationId: x.Id, KidAccountId: x.KidAccountId, Description: x.Description,
                    DueDate: x.DueDate, Amount: x.Amount,
                    OperationType: (GetKidAccountResponse_OperationType)x.OperationType))
                .ToList());
    }

    private static KidAccountSpecification Map(GetKidAccountQuery query)
    {
        return new KidAccountSpecification(Id: query.KidAccountId, KidWalletId: query.KidWalletId, Name: query.Name,
            Balance: query.Balance, CreatedAt: query.CreatedAt, UpdatedAt: query.UpdatedAt,
            IncludeKidAccountOperations: query.IncludeKidAccountOperations);
    }
}