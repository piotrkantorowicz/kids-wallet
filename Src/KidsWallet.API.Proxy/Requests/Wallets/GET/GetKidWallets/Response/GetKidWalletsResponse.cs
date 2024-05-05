namespace KidsWallet.API.Proxy.Requests.Wallets.GET.GetKidWallets.Response;

public sealed record GetKidWalletsResponse(
    Guid KidWalletId,
    Guid KidId,
    string? Name,
    List<GetKidWalletsResponse_KidAccount>? KidAccounts);

public sealed record GetKidWalletsResponse_KidAccount(
    Guid KidAccountId,
    Guid KidWalletId,
    string? Name,
    decimal Balance,
    IReadOnlyCollection<GetKidWalletsResponse_KidAccountOperation> Operations);

public sealed record GetKidWalletsResponse_KidAccountOperation(
    Guid KidAccountOperationId,
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