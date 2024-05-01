using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;

namespace KidsWallet.API.Proxy.Tests.E2E;

internal sealed class KidsWalletApplication : WebApplicationFactory<Program>
{
    private const string Environment = "E2E";
    
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment(Environment);
    }
}