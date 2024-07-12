using NUnit.Framework;

namespace KidsWallet.API.Tests.E2E.Wallets.GET;

internal sealed class GetWallets : WalletsTestBase
{
    [Test]
    public async Task Should_Returns200_OnHappyPath()
    {
        // Arrange
        // Act
        await WebApp.Host.Scenario(configure: x =>
        {
            x.Get.Url(relativeUrl: $"/v1/wallets?id={_walletId}");

            // Assert
            x.StatusCodeShouldBe(statusCode: 200);
        });
    }
}