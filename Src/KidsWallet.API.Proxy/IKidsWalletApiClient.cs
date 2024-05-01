using KidsWallet.API.Proxy.Requests.Accounts;
using KidsWallet.API.Proxy.Requests.Operations;
using KidsWallet.API.Proxy.Requests.Wallets;

namespace KidsWallet.API.Proxy;

public interface IKidsWalletApiClient : IDisposable
{
    IAccountsApi GetAccountsApi();
    
    IOperationsApi GetOperationsApi();
    
    IWalletsApi GetWalletsApi();
}