namespace KidsWallet.API.Proxy.Configuration;

internal sealed class KidsWalletClientFactory : IKidsWalletClientFactory
{
    private readonly KidsWalletApiSettings _apiSettings;
    private readonly IHttpClientFactory _httpClientFactory;
    
    public KidsWalletClientFactory(KidsWalletApiSettings apiSettings, IHttpClientFactory httpClientFactory)
    {
        _apiSettings = apiSettings ?? throw new ArgumentNullException(nameof(apiSettings));
        _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
    }
    
    public IKidsWalletApiClient CreateClient()
    {
        return new KidsWalletApiClient(_apiSettings, _httpClientFactory);
    }
    
    public IKidsWalletApiClient CreateClient(HttpClient httpClient)
    {
        return new KidsWalletApiClient(_apiSettings, httpClient);
    }
}