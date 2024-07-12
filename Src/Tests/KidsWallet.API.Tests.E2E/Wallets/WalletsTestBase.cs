using Bogus;

using KidsWallet.API.Proxy.Requests.Wallets.POST.CreateKidWallet.Request;

namespace KidsWallet.API.Tests.E2E.Wallets;

internal abstract class WalletsTestBase
{
    protected readonly Faker _faker = new();
    protected Guid _walletId;

    protected async Task CreateWallet()
    {
        await WebApp.Host.Scenario(configure: x =>
        {
            // Arrange
            _walletId = _faker.Random.Guid();
            Guid kidId = _faker.Random.Guid();
            string? name = _faker.Random.String2(length: 10);
            CreateKidWalletRequest createRequest = new(KidWalletId: _walletId, KidId: kidId, Name: name);

            // Act
            x.Post.Json(input: createRequest).ToUrl(url: "/v1/wallets");

            // Assert
            x.StatusCodeShouldBe(statusCode: 200);
        });
    }
}