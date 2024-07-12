using NUnit.Framework;

namespace KidsWallet.API.Tests.E2E.Wallets.GET;

internal sealed class GetWallet : WalletsTestBase
{
    [Test]
    public async Task Should_Returns200_OnHappyPath()
    {
        // Arrange
        await CreateWallet();

        // Act
        await WebApp.Host.Scenario(configure: x =>
        {
            x.Get.Url(relativeUrl: $"/v1/wallets/{_walletId}");

            // Assert
            x.StatusCodeShouldBe(statusCode: 200);
        });
    }
}