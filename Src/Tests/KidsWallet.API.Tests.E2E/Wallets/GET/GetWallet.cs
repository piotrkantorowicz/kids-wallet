using NUnit.Framework;

namespace KidsWallet.Tests.E2E.Wallets.GET;

internal sealed class GetWallet : WalletsTestBase
{
    [Test]
    public async Task Should_Returns200_OnHappyPath()
    {
        // Arrange
        await CreateWallet();
        
        // Act
        await WebApp.Host.Scenario(x =>
        {
            x.Get.Url($"/v1/wallets/{_walletId}");
            
            // Assert
            x.StatusCodeShouldBe(200);
        });
    }
}