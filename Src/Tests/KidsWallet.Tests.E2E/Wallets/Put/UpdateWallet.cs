using KidsWallet.Commands.Wallets;

namespace KidsWallet.Tests.E2E.Wallets.Put;

internal sealed class UpdateWallet : WalletsTestBase
{
    [Test]
    public async Task Should_Returns200_OnHappyPath()
    {
        // Arrange
        await CreateWallet();
        
        await WebApp.Host.Scenario(async x =>
        {
            string? newName = _faker.Random.String2(15);
            
            // Act
            UpdateKidWalletRequest updateRequest = new(newName);
            x.Put.Json(updateRequest).ToUrl($"/wallets/{_walletId}");
            
            // Assert
            x.StatusCodeShouldBe(200);
        });
    }
}