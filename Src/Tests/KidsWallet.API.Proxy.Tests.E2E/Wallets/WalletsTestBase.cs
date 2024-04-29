using Bogus;

using FluentAssertions;

using KidsWallet.API.Proxy.Requests.Wallets.POST.CreateKidWallet.Request;

namespace KidsWallet.API.Proxy.Tests.E2E.Wallets;

public abstract class WalletsTestBase
{
    protected readonly Faker _faker = new();
    protected Guid _walletId;
    
    protected async Task CreateWallet()
    {
        _walletId = _faker.Random.Guid();
        
        CreateKidWalletRequest createKidAccountRequest = new(_walletId,
            _faker.Random.Guid(), _faker.Random.String2(15));
        
        // Act
        Func<Task> act = async () => await WebAppClient.WalletsApi.CreateWallet(createKidAccountRequest);
        
        // Assert
        await act.Should().NotThrowAsync();
    }
}