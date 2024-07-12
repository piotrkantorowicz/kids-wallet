using JasperFx.Core;

namespace KidsWallet.API.Proxy.Tests.E2E;

internal sealed class WebApp
{
    private static readonly Lazy<WebApp> Lazy = new(valueFactory: () => new WebApp());
    private readonly KidsWalletApplication _kidsWalletApplication;
    private HttpClient? _httpClient;

    private WebApp()
    {
        KidsWalletApplication app = new();
        _kidsWalletApplication = app;
        _httpClient = app.CreateClient();
    }

    public static WebApp Instance => Lazy.Value;

    public HttpClient GetHttpClient()
    {
        _httpClient = _kidsWalletApplication.CreateClient();

        return _httpClient;
    }

    public void Destroy()
    {
        _httpClient?.SafeDispose();
        _kidsWalletApplication.SafeDispose();
    }
}