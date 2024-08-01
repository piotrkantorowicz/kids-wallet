namespace KidsWallet.API.Proxy.Requests.Wallets.POST.CreateKidWallet.Request;

public sealed record CreateKidWalletRequest(Guid KidWalletId, Guid KidId, string Name);