using Bogus.Extensions;

using KidsWallet.API.Proxy.Requests.Accounts.POST.CreateKidAccount.Request;
using KidsWallet.Tests.E2E.Wallets;

namespace KidsWallet.Tests.E2E.Accounts;

internal abstract class AccountsTestBase : WalletsTestBase
{
    protected Guid _accountId;
    
    protected async Task CreateAccount()
    {
        // Arrange
        await CreateWallet();
        
        await WebApp.Host.Scenario(x =>
        {
            _accountId = _faker.Random.Guid();
            string? accountName = _faker.Random.String2(10);
            decimal balance = _faker.Random.Decimal2(150, 1000);
            CreateKidAccountRequest createRequest = new(_accountId, _walletId, accountName, balance);
            
            // Act
            x.Post.Json(createRequest).ToUrl("/v1/accounts");
            
            // Assert
            x.StatusCodeShouldBe(200);
        });
    }
}