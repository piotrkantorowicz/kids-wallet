using NUnit.Framework;

namespace KidsWallet.API.Tests.E2E.Wallets.POST;

internal sealed class CreateWallet : WalletsTestBase
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