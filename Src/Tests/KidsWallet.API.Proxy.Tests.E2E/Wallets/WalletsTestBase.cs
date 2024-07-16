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

        CreateKidWalletRequest createKidAccountRequest = new(KidWalletId: _walletId, KidId: _faker.Random.Guid(),
            Name: _faker.Random.String2(length: 15));

        // Act
        Func<Task> act = async () => await WebAppClient.WalletsApi.CreateWallet(model: createKidAccountRequest);

        // Assert
        await act.Should().NotThrowAsync();
    }
}