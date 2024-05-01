using FluentAssertions;

using KidsWallet.API.Proxy.Requests.Accounts.GET.GetKidAccount.Response;

namespace KidsWallet.API.Proxy.Tests.E2E.Accounts.GET;

internal sealed class GetAccount : AccountsTestBase
{
    [Test]
    public async Task Should_NotThrow_OnHappyPath()
    {
        // Arrange
        await CreateAccount();
        
        // Act
        Func<Task<GetKidAccountResponse>> act = async () => await WebAppClient.AccountsApi.GetAccount(_accountId);
        
        // Assert
        await act.Should().NotThrowAsync();
    }
}