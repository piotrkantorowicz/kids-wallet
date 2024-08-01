namespace KidsWallet.API.Proxy.Requests.Wallets.GET.GetKidWallet.Response;

public sealed record GetKidWalletResponse(
    Guid KidWalletId,
    Guid KidId,
    string? Name,
    IReadOnlyCollection<GetKidWalletResponse_KidAccount>? KidAccounts);

public sealed record GetKidWalletResponse_KidAccount(
    Guid KidAccountId,
    Guid KidWalletId,
    string? Name,
    decimal Balance,
    IReadOnlyCollection<GetKidWalletResponse_KidAccountOperation> Operations);

public sealed record GetKidWalletResponse_KidAccountOperation(
    Guid KidAccountOperationId,
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