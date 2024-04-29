namespace KidsWallet.API.Proxy.Accounts.GET.GetKidAccounts.Response;

public sealed record GetKidAccountsResponse(
    Guid Id,
    Guid KidWalletId,
    string? Name,
    decimal Balance,
    IReadOnlyCollection<GetKidAccountsResponse_KidAccountOperation> Operations);

public sealed record GetKidAccountsResponse_KidAccountOperation(
    Guid Id,
    Guid KidAccountId,
    string? Description,
    DateTime DueDate,
    decimal Amount,
    GetKidAccountsResponse_OperationType OperationType);

public enum GetKidAccountsResponse_OperationType
{
    None,
    Income,
    Expense,
    Transfer
}