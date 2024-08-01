namespace KidsWallet.API.Proxy.Requests.Accounts.POST.CreateKidAccount.Request;

public sealed record CreateKidAccountRequest(Guid KidAccountId, Guid KidWalletId, string Name, decimal Balance);