namespace KidsWallet.API.Proxy.Configuration;

public sealed class KidsWalletApiSettings
{
    public string? BaseUrl { get; set; }
    
    public int Timeout { get; set; }
}