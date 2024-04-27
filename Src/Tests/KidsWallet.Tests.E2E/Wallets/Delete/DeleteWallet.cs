using NUnit.Framework;

namespace KidsWallet.Tests.E2E.Wallets.Delete;

internal sealed class DeleteWallet : WalletsTestBase
{
    [Test]
    public async Task Should_Returns200_OnHappyPath()
    {
        // Arrange
        await CreateWallet();
        
        await WebApp.Host.Scenario(x =>
        {
            // Act
            x.Delete.Url($"/wallets/{_walletId}");
            
            // Assert
            x.StatusCodeShouldBe(200);
        });
    }
}