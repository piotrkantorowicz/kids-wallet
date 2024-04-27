using NUnit.Framework;

namespace KidsWallet.Tests.E2E.Accounts.Delete;

internal sealed class DeleteAccount : AccountsTestBase
{
    [Test]
    public async Task Should_Returns200_OnHappyPath()
    {
        // Arrange
        await CreateAccount();
        
        await WebApp.Host.Scenario(x =>
        {
            // Act
            x.Delete.Url($"/accounts/{_accountId}");
            
            // Assert
            x.StatusCodeShouldBe(200);
        });
    }
}