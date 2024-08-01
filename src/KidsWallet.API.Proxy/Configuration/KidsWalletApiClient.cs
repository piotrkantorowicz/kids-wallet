using KidsWallet.API.Proxy.Requests.Accounts;
using KidsWallet.API.Proxy.Requests.Operations;
using KidsWallet.API.Proxy.Requests.Wallets;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

using RestEase;

namespace KidsWallet.API.Proxy.Configuration;

internal sealed class KidsWalletApiClient : IKidsWalletApiClient
{
    private static readonly JsonSerializerSettings JsonSerializerSettings = new()
    {
        ContractResolver = new DefaultContractResolver
        {
            NamingStrategy = new CamelCaseNamingStrategy()
        },
        NullValueHandling = NullValueHandling.Ignore,
        DateTimeZoneHandling = DateTimeZoneHandling.Utc,
        ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
        DateParseHandling = DateParseHandling.DateTime
    };

    private readonly HttpClient _httpClient;

    public KidsWalletApiClient(KidsWalletApiSettings settings, IHttpClientFactory httpClientFactory)
    {
        ArgumentNullException.ThrowIfNull(argument: settings);

        if (settings.BaseUrl is null)
        {
            throw new ArgumentException(message: nameof(settings.BaseUrl));
        }

        _httpClient = httpClientFactory.CreateClient();
        _httpClient.BaseAddress = new Uri(uriString: settings.BaseUrl);
        _httpClient.Timeout = TimeSpan.FromMilliseconds(value: settings.Timeout);
    }

    public KidsWalletApiClient(KidsWalletApiSettings settings, HttpClient httpClient)
    {
        ArgumentNullException.ThrowIfNull(argument: settings);

        if (settings.BaseUrl is null)
        {
            throw new ArgumentException(message: nameof(settings.BaseUrl));
        }

        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri(uriString: settings.BaseUrl);
        _httpClient.Timeout = TimeSpan.FromMilliseconds(value: settings.Timeout);
    }

    public IAccountsApi GetAccountsApi()
    {
        return GetApiClient<IAccountsApi>();
    }

    public IOperationsApi GetOperationsApi()
    {
        return GetApiClient<IOperationsApi>();
    }

    public IWalletsApi GetWalletsApi()
    {
        return GetApiClient<IWalletsApi>();
    }

    public void Dispose()
    {
        _httpClient.Dispose();
    }

    private TApi GetApiClient<TApi>()
    {
        TApi apiClient = new RestClient(httpClient: _httpClient)
        {
            JsonSerializerSettings = JsonSerializerSettings
        }.For<TApi>();

        return apiClient;
    }
}