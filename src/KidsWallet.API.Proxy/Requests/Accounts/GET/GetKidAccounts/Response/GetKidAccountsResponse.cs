namespace KidsWallet.API.Proxy.Requests.Accounts.GET.GetKidAccounts.Response;

public sealed record GetKidAccountsResponse(
    Guid KidAccountId,
    Guid KidWalletId,
    string? Name,
    decimal Balance,
    IReadOnlyCollection<GetKidAccountsResponse_KidAccountOperation> Operations);

public sealed record GetKidAccountsResponse_KidAccountOperation(
    Guid KidAccountOperationId,
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