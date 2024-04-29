using NUnit.Framework;

namespace KidsWallet.Tests.E2E.Accounts.GET;

internal sealed class GetAccount : AccountsTestBase
{
    [Test]
    public async Task Should_Returns200_OnHappyPath()
    {
        // Arrange
        await CreateAccount();
        
        await WebApp.Host.Scenario(x =>
        {
            // Act
            x.Get.Url($"/v1/accounts/{_accountId}");
            
            // Assert
            x.StatusCodeShouldBe(200);
        });
    }
}