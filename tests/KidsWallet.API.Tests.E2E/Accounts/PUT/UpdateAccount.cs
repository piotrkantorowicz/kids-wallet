using Bogus.Extensions;

using KidsWallet.API.Proxy.Requests.Accounts.PUT.UpdateKidAccounts.Request;

using NUnit.Framework;

namespace KidsWallet.API.Tests.E2E.Accounts.PUT;

internal sealed class UpdateAccount : AccountsTestBase
{
    [Test]
    public async Task Should_Returns200_OnHappyPath()
    {
        // Arrange
        await CreateAccount();

        await WebApp.Host.Scenario(configure: x =>
        {
            string? newAccountName = _faker.Random.String2(length: 10);
            decimal newBalance = _faker.Random.Decimal2(min: 150, max: 1000);
            UpdateKidAccountRequest updateKidAccountRequest = new(Name: newAccountName, Balance: newBalance);

            // Act
            x.Put.Json(input: updateKidAccountRequest).ToUrl(url: $"/v1/accounts/{_accountId}");

            // Assert
            x.StatusCodeShouldBe(statusCode: 200);
        });
    }
}