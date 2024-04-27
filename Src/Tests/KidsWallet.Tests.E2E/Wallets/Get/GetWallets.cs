using NUnit.Framework;

namespace KidsWallet.Tests.E2E.Wallets.Get;

internal sealed class GetWallets : WalletsTestBase
{
    [Test]
    public async Task Should_Returns200_OnHappyPath()
    {
        // Arrange
        // Act
        await WebApp.Host.Scenario(x =>
        {
            x.Get.Url($"/wallets?id={_walletId}");
            
            // Assert
            x.StatusCodeShouldBe(200);
        });
    }
}