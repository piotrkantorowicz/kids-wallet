namespace KidsWallet.API.Proxy.Requests.Wallets.POST.CreateKidWallet.Request;

public sealed record CreateKidWalletRequest(Guid WalletId, Guid KidId, string Name);