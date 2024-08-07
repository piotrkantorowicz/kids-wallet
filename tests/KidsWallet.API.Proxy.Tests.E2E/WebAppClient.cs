﻿using KidsWallet.API.Proxy.Configuration;
using KidsWallet.API.Proxy.Configuration.Di;
using KidsWallet.API.Proxy.Requests.Accounts;
using KidsWallet.API.Proxy.Requests.Operations;
using KidsWallet.API.Proxy.Requests.Wallets;

using Microsoft.Extensions.DependencyInjection;

namespace KidsWallet.API.Proxy.Tests.E2E;

[SetUpFixture]
internal class WebAppClient
{
    public static IAccountsApi AccountsApi { get; private set; } = null!;

    public static IOperationsApi OperationsApi { get; private set; } = null!;

    public static IWalletsApi WalletsApi { get; private set; } = null!;

    [OneTimeSetUp]
    public void Init()
    {
        HttpClient httpClient = WebApp.Instance.GetHttpClient();

        ServiceProvider serviceProvider = new ServiceCollection()
            .AddKidsWalletProxy(apiSettings: new KidsWalletApiSettings
            {
                BaseUrl = "http://localhost:5164",
                Timeout = 5000
            }, httpClient: httpClient)
            .BuildServiceProvider();

        IKidsWalletApiClient kidsWalletApiClient = serviceProvider.GetRequiredService<IKidsWalletApiClient>();
        AccountsApi = kidsWalletApiClient.GetAccountsApi();
        OperationsApi = kidsWalletApiClient.GetOperationsApi();
        WalletsApi = kidsWalletApiClient.GetWalletsApi();
    }

    [OneTimeTearDown]
    public void Teardown()
    {
        WebApp.Instance.Destroy();
    }
}