using KidsWallet.API.Proxy.Requests.Wallets.PUT.UpdateKidWallet.Request;

using NUnit.Framework;

namespace KidsWallet.Tests.E2E.Wallets.PUT;

internal sealed class UpdateWallet : WalletsTestBase
{
    [Test]
    public async Task Should_Returns200_OnHappyPath()
    {
        // Arrange
        await CreateWallet();
        
        await WebApp.Host.Scenario(x =>
        {
            string? newName = _faker.Random.String2(15);
            
            // Act
            UpdateKidWalletRequest updateRequest = new(newName);
            x.Put.Json(updateRequest).ToUrl($"/v1/wallets/{_walletId}");
            
            // Assert
            x.StatusCodeShouldBe(200);
        });
    }
}