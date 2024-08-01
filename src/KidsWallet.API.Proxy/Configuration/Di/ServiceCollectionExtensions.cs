using Microsoft.Extensions.DependencyInjection;

namespace KidsWallet.API.Proxy.Configuration.Di;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddKidsWalletProxy(this IServiceCollection services,
        KidsWalletApiSettings apiSettings, HttpClient? httpClient = null)
    {
        services.AddHttpClient();
        services.AddSingleton(implementationInstance: apiSettings);
        services.AddSingleton<IKidsWalletClientFactory, KidsWalletClientFactory>();

        services.AddScoped(serviceType: typeof(IKidsWalletApiClient), implementationFactory: provider =>
        {
            IKidsWalletClientFactory kidsWalletClientFactory = provider.GetRequiredService<IKidsWalletClientFactory>();

            return httpClient is null
                ? kidsWalletClientFactory.CreateClient()
                : kidsWalletClientFactory.CreateClient(httpClient: httpClient);
        });

        return services;
    }
}