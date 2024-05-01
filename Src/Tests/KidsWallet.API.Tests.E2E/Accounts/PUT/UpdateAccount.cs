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
        
        await WebApp.Host.Scenario(x =>
        {
            string? newAccountName = _faker.Random.String2(10);
            decimal newBalance = _faker.Random.Decimal2(150, 1000);
            UpdateKidAccountRequest updateKidAccountRequest = new(newAccountName, newBalance);
            
            // Act
            x.Put.Json(updateKidAccountRequest).ToUrl($"/v1/accounts/{_accountId}");
            
            // Assert
            x.StatusCodeShouldBe(200);
        });
    }
}