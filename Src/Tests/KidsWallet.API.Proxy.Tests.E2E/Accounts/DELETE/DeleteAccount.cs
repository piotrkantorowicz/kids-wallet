using FluentAssertions;

namespace KidsWallet.API.Proxy.Tests.E2E.Accounts.DELETE;

internal sealed class DeleteAccount : AccountsTestBase
{
    [Test]
    public async Task Should_NotThrow_OnHappyPath()
    {
        // Arrange
        await CreateAccount();
        
        // Act
        Func<Task> act = async () => await WebAppClient.AccountsApi.DeleteAccount(id: _accountId);
        
        // Assert
        await act.Should().NotThrowAsync();
    }
}