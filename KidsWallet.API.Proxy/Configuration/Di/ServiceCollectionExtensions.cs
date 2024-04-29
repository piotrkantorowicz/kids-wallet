using Microsoft.Extensions.DependencyInjection;

namespace KidsWallet.API.Proxy.Configuration;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddKidsWalletProxy(this IServiceCollection services)
    {
        services.AddHttpClient();
        services.AddSingleton<IKidsWalletClientFactory, KidsWalletClientFactory>();
        
        services.AddScoped(typeof(IKidsWalletApiClient), provider =>
        {
            var kidsWalletClientFactory = provider.GetRequiredService<IKidsWalletClientFactory>();
            
            return kidsWalletClientFactory.CreateClient();
        });
        
        return services;
    }
}