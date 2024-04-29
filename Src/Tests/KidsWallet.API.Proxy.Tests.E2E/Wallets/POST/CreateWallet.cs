namespace KidsWallet.API.Proxy.Tests.E2E.Wallets.POST;

internal sealed class CreateWallet : WalletsTestBase
{
    [Test]
    public async Task Should_NotThrow_OnHappyPath()
    {
        // Arrange
        // Act
        // Assert
        await CreateWallet();
    }
}