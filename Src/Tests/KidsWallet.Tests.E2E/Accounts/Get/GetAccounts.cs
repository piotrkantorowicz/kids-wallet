namespace KidsWallet.Tests.E2E.Accounts.Get;

internal sealed class GetAccounts : AccountsTestBase
{
    [Test]
    public async Task Should_Returns200_OnHappyPath()
    {
        // Arrange
        await CreateAccount();
        
        await WebApp.Host.Scenario(x =>
        {
            // Act
            x.Get.Url($"/accounts?id={_accountId}");
            
            // Assert
            x.StatusCodeShouldBe(200);
        });
    }
}