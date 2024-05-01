using NUnit.Framework;

namespace KidsWallet.API.Tests.E2E.Accounts.POST;

internal sealed class CreateAccount : AccountsTestBase
{
    [Test]
    public async Task Should_Returns200_OnHappyPath()
    {
        // Arrange
        // Act
        // Assert
        await CreateAccount();
    }
}