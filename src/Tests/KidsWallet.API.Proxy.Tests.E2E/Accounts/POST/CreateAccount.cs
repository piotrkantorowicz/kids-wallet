namespace KidsWallet.API.Proxy.Tests.E2E.Accounts.POST;

internal sealed class CreateAccount : AccountsTestBase
{
    [Test]
    public async Task Should_NotThrow_OnHappyPath()
    {
        // Arrange
        // Act
        // Assert
        await CreateAccount();
    }
}