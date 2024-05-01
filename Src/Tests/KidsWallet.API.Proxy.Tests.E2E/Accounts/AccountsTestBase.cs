using FluentAssertions;

using KidsWallet.API.Proxy.Requests.Accounts.POST.CreateKidAccount.Request;
using KidsWallet.API.Proxy.Tests.E2E.Wallets;

namespace KidsWallet.API.Proxy.Tests.E2E.Accounts;

internal abstract class AccountsTestBase : WalletsTestBase
{
    protected Guid _accountId;
    
    protected async Task CreateAccount()
    {
        // Arrange
        await CreateWallet();
        _accountId = _faker.Random.Guid();
        
        CreateKidAccountRequest createKidAccountRequest = new(_accountId, _walletId,
            _faker.Random.String2(30), _faker.Random.Decimal());
        
        // Act
        Func<Task> act = async () => await WebAppClient.AccountsApi.CreateAccount(createKidAccountRequest);
        
        // Assert
        await act.Should().NotThrowAsync();
    }
}