namespace KidsWallet.API.Proxy.Wallets.GET.GetKidWallets.Response;

public sealed record GetKidWalletsResponse(
    Guid Id,
    Guid KidId,
    string? Name,
    List<GetKidWalletsResponse_KidAccount>? KidAccounts);

public sealed record GetKidWalletsResponse_KidAccount(
    Guid Id,
    Guid KidWalletId,
    string? Name,
    decimal Balance,
    IReadOnlyCollection<GetKidWalletsResponse_KidAccountOperation> Operations);

public sealed record GetKidWalletsResponse_KidAccountOperation(
    Guid Id,
    Guid KidAccountId,
    string? Description,
    DateTime DueDate,
    decimal Amount,
    GetKidWalletsResponse_OperationType OperationType);

public enum GetKidWalletsResponse_OperationType
{
    None,
    Income,
    Expense,
    Transfer
}