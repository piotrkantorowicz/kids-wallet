namespace KidsWallet.Tests.E2E.Wallets.Post;

internal sealed class AddWallet : WalletsTestBase
{
    [Test]
    public async Task Should_Returns200_OnHappyPath()
    {
        // Arrange
        // Act
        // Assert
        await CreateWallet();
    }
}