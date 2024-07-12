using KidsWallet.API.Proxy.Requests.Accounts.GET.GetKidAccounts.Response;
using KidsWallet.Persistence.Model.Accounts;
using KidsWallet.Persistence.Model.Accounts.Specifications;
using KidsWallet.Services.Abstraction;

namespace KidsWallet.Queries.Accounts;

public sealed record GetKidAccountsQuery(
    Guid? KidAccountId = default,
    Guid? KidWalletId = default,
    string? Name = default,
    decimal? Balance = default,
    DateTime? CreatedAt = default,
    DateTime? UpdatedAt = default,
    bool? IncludeKidAccountOperations = default);

public static class GetKidAccountsQueryQueryHandler
{
    public static async Task<IReadOnlyCollection<GetKidAccountsResponse>> Handle(GetKidAccountsQuery query,
        ICrudOperationsService<KidAccount> kidWalletCrudOperationsService, CancellationToken cancellationToken)
    {
        IReadOnlyCollection<KidAccount> kidAccount =
            await kidWalletCrudOperationsService.GetManyAsync(specification: Map(query: query),
                cancellationToken: cancellationToken);

        return Map(kidAccounts: kidAccount);
    }

    private static IReadOnlyCollection<GetKidAccountsResponse> Map(IEnumerable<KidAccount> kidAccounts)
    {
        return kidAccounts
            .Select(selector: x => new GetKidAccountsResponse(KidAccountId: x.Id, KidWalletId: x.KidWalletId,
                Name: x.Name, Balance: x.Balance,
                Operations: x
                    .KidAccountOperations.Select(selector: y => new GetKidAccountsResponse_KidAccountOperation(
                        KidAccountOperationId: y.Id, KidAccountId: y.KidAccountId, Description: y.Description,
                        DueDate: y.DueDate, Amount: y.Amount,
                        OperationType: (GetKidAccountsResponse_OperationType)y.OperationType))
                    .ToList()))
            .ToList()
            .AsReadOnly();
    }

    private static KidAccountSpecification Map(GetKidAccountsQuery query)
    {
        return new KidAccountSpecification(Id: query.KidAccountId, KidWalletId: query.KidWalletId, Name: query.Name,
            Balance: query.Balance, CreatedAt: query.CreatedAt, UpdatedAt: query.UpdatedAt,
            IncludeKidAccountOperations: query.IncludeKidAccountOperations);
    }
}