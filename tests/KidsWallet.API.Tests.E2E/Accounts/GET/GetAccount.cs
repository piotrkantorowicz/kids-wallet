using NUnit.Framework;

namespace KidsWallet.API.Tests.E2E.Accounts.GET;

internal sealed class GetAccount : AccountsTestBase
{
    [Test]
    public async Task Should_Returns200_OnHappyPath()
    {
        // Arrange
        await CreateAccount();

        await WebApp.Host.Scenario(configure: x =>
        {
            // Act
            x.Get.Url(relativeUrl: $"/v1/accounts/{_accountId}");

            // Assert
            x.StatusCodeShouldBe(statusCode: 200);
        });
    }
}