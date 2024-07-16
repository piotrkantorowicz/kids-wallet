using Bogus.Extensions;

using KidsWallet.API.Proxy.Requests.Accounts.POST.CreateKidAccount.Request;
using KidsWallet.API.Tests.E2E.Wallets;

namespace KidsWallet.API.Tests.E2E.Accounts;

internal abstract class AccountsTestBase : WalletsTestBase
{
    protected Guid _accountId;

    protected async Task CreateAccount()
    {
        // Arrange
        await CreateWallet();

        await WebApp.Host.Scenario(configure: x =>
        {
            _accountId = _faker.Random.Guid();
            string? accountName = _faker.Random.String2(length: 10);
            decimal balance = _faker.Random.Decimal2(min: 150, max: 1000);

            CreateKidAccountRequest createRequest = new(KidAccountId: _accountId, WalletId: _walletId,
                Name: accountName, Balance: balance);

            // Act
            x.Post.Json(input: createRequest).ToUrl(url: "/v1/accounts");

            // Assert
            x.StatusCodeShouldBe(statusCode: 200);
        });
    }
}