namespace KidsWallet.API.Proxy.Requests.Accounts.GET.GetKidAccount.Response;

public sealed record GetKidAccountResponse(
    Guid KidAccountId,
    Guid KidWalletId,
    string? Name,
    decimal Balance,
    IReadOnlyCollection<GetKidAccountResponse_KidAccountOperation> Operations);

public sealed record GetKidAccountResponse_KidAccountOperation(
    Guid KidAccountOperationId,
    Guid KidAccountId,
    string? Description,
    DateTime DueDate,
    decimal Amount,
    GetKidAccountResponse_OperationType OperationType);

public enum GetKidAccountResponse_OperationType
{
    None,
    Income,
    Expense,
    Transfer
}