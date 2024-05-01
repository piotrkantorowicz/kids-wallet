namespace KidsWallet.API.Proxy.Requests.Wallets.GET.GetKidWallet.Response;

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