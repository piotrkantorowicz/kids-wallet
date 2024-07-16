using KidsWallet.API.Proxy.Requests.Wallets.GET.GetKidWallet.Response;
using KidsWallet.Persistence.Model.Wallets;
using KidsWallet.Persistence.Model.Wallets.Specifications;
using KidsWallet.Services.Abstraction;

namespace KidsWallet.Queries.Wallets;

public sealed record GetKidWalletQuery(
    Guid? KidWalletId = default,
    Guid? KidId = default,
    string? Name = default,
    DateTime? CreatedAt = default,
    DateTime? UpdatedAt = default,
    bool? IncludeKidAccounts = default,
    bool? IncludeKidAccountOperations = default);

public static class GetKidWalletQueryQueryHandler
{
    public static async Task<GetKidWalletResponse?> Handle(GetKidWalletQuery query,
        ICrudOperationsService<KidWallet> kidWalletCrudOperationsService, CancellationToken cancellationToken)
    {
        KidWallet? kidWallet = await kidWalletCrudOperationsService.GetAsync(specification: Map(query: query),
            throwWhenNotFound: true, cancellationToken: cancellationToken);

        return Map(kidWallet: kidWallet);
    }

    private static GetKidWalletResponse? Map(KidWallet? kidWallet)
    {
        if (kidWallet is null)
        {
            return null;
        }

        return new GetKidWalletResponse(KidWalletId: kidWallet.Id, KidId: kidWallet.KidId, Name: kidWallet.Name,
            KidAccounts: kidWallet
                .KidAccounts?.Select(selector: x => new GetKidWalletResponse_KidAccount(KidAccountId: x.Id,
                    KidWalletId: x.KidWalletId, Name: x.Name, Balance: x.Balance,
                    Operations: x
                        .KidAccountOperations.Select(selector: y =>
                            new GetKidWalletResponse_KidAccountOperation(KidAccountOperationId: y.Id,
                                KidAccountId: y.KidAccountId, Description: y.Description, DueDate: y.DueDate,
                                Amount: y.Amount, OperationType: (GetKidWalletResponse_OperationType)y.OperationType))
                        .ToList()))
                .ToList());
    }

    private static KidWalletSpecification Map(GetKidWalletQuery query)
    {
        return new KidWalletSpecification(Id: query.KidWalletId, KidId: query.KidId, Name: query.Name,
            CreatedAt: query.CreatedAt, UpdatedAt: query.UpdatedAt, IncludeKidAccounts: query.IncludeKidAccounts,
            IncludeKidAccountOperations: query.IncludeKidAccountOperations);
    }
}