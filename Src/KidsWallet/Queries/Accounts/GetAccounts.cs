﻿using KidsWallet.API.Proxy.Requests.Accounts.GET.GetKidAccounts.Response;
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
            await kidWalletCrudOperationsService.GetManyAsync(Map(query), cancellationToken);
        
        return Map(kidAccount);
    }
    
    private static IReadOnlyCollection<GetKidAccountsResponse> Map(IEnumerable<KidAccount> kidAccounts)
    {
        return kidAccounts
            .Select(x => new GetKidAccountsResponse(x.Id, x.KidWalletId, x.Name, x.Balance,
                x
                    .KidAccountOperations.Select(y => new GetKidAccountsResponse_KidAccountOperation(y.Id,
                        y.KidAccountId, y.Description, y.DueDate, y.Amount,
                        (GetKidAccountsResponse_OperationType)y.OperationType))
                    .ToList()))
            .ToList()
            .AsReadOnly();
    }
    
    private static KidAccountSpecification Map(GetKidAccountsQuery query)
    {
        return new KidAccountSpecification(query.KidAccountId, query.KidWalletId, query.Name, query.Balance,
            query.CreatedAt, query.UpdatedAt, query.IncludeKidAccountOperations);
    }
}