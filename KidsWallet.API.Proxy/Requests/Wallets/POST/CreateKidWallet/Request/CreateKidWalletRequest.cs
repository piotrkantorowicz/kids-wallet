namespace KidsWallet.API.Proxy.Wallets.POST.CreateKidWallet.Request;

public sealed record CreateKidWalletRequest(Guid WalletId, Guid KidId, string Name);