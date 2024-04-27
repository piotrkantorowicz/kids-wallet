﻿using KidsWallet.Persistence.Model.Wallets;
using KidsWallet.Persistence.Model.Wallets.Specifications;
using KidsWallet.Services.Abstraction;

namespace KidsWallet.Queries.Wallets;

public sealed record GetKidWalletResponse(
    Guid Id,
    Guid KidId,
    string? Name,
    IReadOnlyCollection<GetKidWalletResponse_KidAccount>? KidAccounts);

public sealed record GetKidWalletResponse_KidAccount(
    Guid Id,
    Guid KidWalletId,
    string? Name,
    decimal Balance,
    IReadOnlyCollection<GetKidWalletResponse_KidAccountOperation> Operations);

public sealed record GetKidWalletResponse_KidAccountOperation(
    Guid Id,
    Guid KidAccountId,
    string? Description,
    DateTime DueDate,
    decimal Amount,
    GetKidWalletResponse_OperationType OperationType);

public enum GetKidWalletResponse_OperationType
{
    None,
    Income,
    Expense,
    Transfer
}

public sealed record GetKidWalletQuery(
    Guid? Id = default,
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
        KidWallet? kidWallet = await kidWalletCrudOperationsService.GetAsync(Map(query), true, cancellationToken);
        
        return Map(kidWallet);
    }
    
    private static GetKidWalletResponse? Map(KidWallet? kidWallet)
    {
        if (kidWallet is null)
        {
            return null;
        }
        
        return new GetKidWalletResponse(kidWallet.Id, kidWallet.KidId, kidWallet.Name,
            kidWallet
                .KidAccounts?.Select(x => new GetKidWalletResponse_KidAccount(x.Id, x.KidWalletId, x.Name, x.Balance,
                    x
                        .KidAccountOperations.Select(y => new GetKidWalletResponse_KidAccountOperation(y.Id,
                            y.KidAccountId, y.Description, y.DueDate, y.Amount,
                            (GetKidWalletResponse_OperationType)y.OperationType))
                        .ToList()))
                .ToList());
    }
    
    private static KidWalletSpecification Map(GetKidWalletQuery query)
    {
        return new KidWalletSpecification(query.Id, query.KidId, query.Name, query.CreatedAt, query.UpdatedAt,
            query.IncludeKidAccounts, query.IncludeKidAccountOperations);
    }
}