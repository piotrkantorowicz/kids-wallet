using Microsoft.Extensions.DependencyInjection;

namespace KidsWallet.API.Proxy.Configuration.Di;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddKidsWalletProxy(this IServiceCollection services,
        KidsWalletApiSettings apiSettings, HttpClient? httpClient = null)
    {
        services.AddHttpClient();
        services.AddSingleton(apiSettings);
        services.AddSingleton<IKidsWalletClientFactory, KidsWalletClientFactory>();
        
        services.AddScoped(typeof(IKidsWalletApiClient), provider =>
        {
            IKidsWalletClientFactory kidsWalletClientFactory = provider.GetRequiredService<IKidsWalletClientFactory>();
            
            return httpClient is null
                ? kidsWalletClientFactory.CreateClient()
                : kidsWalletClientFactory.CreateClient(httpClient);
        });
        
        return services;
    }
}