using NUnit.Framework;

namespace KidsWallet.Tests.E2E.Wallets.GET;

internal sealed class GetWallets : WalletsTestBase
{
    [Test]
    public async Task Should_Returns200_OnHappyPath()
    {
        // Arrange
        // Act
        await WebApp.Host.Scenario(x =>
        {
            x.Get.Url($"/v1/wallets?id={_walletId}");
            
            // Assert
            x.StatusCodeShouldBe(200);
        });
    }
}