namespace KidsWallet.API.Proxy;

public interface IKidsWalletClientFactory
{
    IKidsWalletApiClient CreateClient();
    
    IKidsWalletApiClient CreateClient(HttpClient httpClient);
}