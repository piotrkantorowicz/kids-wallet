using KidsWallet.API.Proxy.Requests.Wallets.PUT.UpdateKidWallet.Request;

using NUnit.Framework;

namespace KidsWallet.API.Tests.E2E.Wallets.PUT;

internal sealed class UpdateWallet : WalletsTestBase
{
    [Test]
    public async Task Should_Returns200_OnHappyPath()
    {
        // Arrange
        await CreateWallet();

        await WebApp.Host.Scenario(configure: x =>
        {
            string? newName = _faker.Random.String2(length: 15);

            // Act
            UpdateKidWalletRequest updateRequest = new(Name: newName);
            x.Put.Json(input: updateRequest).ToUrl(url: $"/v1/wallets/{_walletId}");

            // Assert
            x.StatusCodeShouldBe(statusCode: 200);
        });
    }
}