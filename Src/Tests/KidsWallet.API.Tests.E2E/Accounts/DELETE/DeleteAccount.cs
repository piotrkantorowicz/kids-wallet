using NUnit.Framework;

namespace KidsWallet.API.Tests.E2E.Accounts.DELETE;

internal sealed class DeleteAccount : AccountsTestBase
{
    [Test]
    public async Task Should_Returns200_OnHappyPath()
    {
        // Arrange
        await CreateAccount();

        await WebApp.Host.Scenario(configure: x =>
        {
            // Act
            x.Delete.Url(relativeUrl: $"/v1/accounts/{_accountId}");

            // Assert
            x.StatusCodeShouldBe(statusCode: 200);
        });
    }
}