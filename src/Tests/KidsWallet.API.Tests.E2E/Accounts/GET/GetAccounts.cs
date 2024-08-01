using NUnit.Framework;

namespace KidsWallet.API.Tests.E2E.Accounts.GET;

internal sealed class GetAccounts : AccountsTestBase
{
    [Test]
    public async Task Should_Returns200_OnHappyPath()
    {
        // Arrange
        await CreateAccount();

        await WebApp.Host.Scenario(configure: x =>
        {
            // Act
            x.Get.Url(relativeUrl: $"/v1/accounts?id={_accountId}");

            // Assert
            x.StatusCodeShouldBe(statusCode: 200);
        });
    }
}