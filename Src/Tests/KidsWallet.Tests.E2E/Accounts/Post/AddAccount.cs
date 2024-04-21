namespace KidsWallet.Tests.E2E.Accounts.Post;

internal sealed class AddAccount : AccountsTestBase
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