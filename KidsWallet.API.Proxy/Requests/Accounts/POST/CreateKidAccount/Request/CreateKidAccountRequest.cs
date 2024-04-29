namespace KidsWallet.API.Proxy.Accounts.POST.CreateKidAccount.Request;

public sealed record CreateKidAccountRequest(Guid AccountId, Guid WalletId, string Name, decimal Balance);