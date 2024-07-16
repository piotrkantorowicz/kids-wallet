using FluentAssertions;

using KidsWallet.API.Proxy.Requests.Accounts.PUT.UpdateKidAccounts.Request;

namespace KidsWallet.API.Proxy.Tests.E2E.Accounts.PUT;

internal sealed class UpdateAccount : AccountsTestBase
{
    [Test]
    public async Task Should_NotThrow_OnHappyPath()
    {
        // Arrange
        await CreateAccount();

        UpdateKidAccountRequest updateKidAccountRequest =
            new(Name: _faker.Random.String2(length: 15), Balance: _faker.Random.Decimal());

        // Act
        Func<Task> act = async () =>
            await WebAppClient.AccountsApi.UpdateAccount(id: _accountId, model: updateKidAccountRequest);

        // Assert
        await act.Should().NotThrowAsync();
    }
}