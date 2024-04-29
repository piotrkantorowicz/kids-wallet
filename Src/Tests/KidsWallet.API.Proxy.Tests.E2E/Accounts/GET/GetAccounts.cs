using FluentAssertions;

using KidsWallet.API.Proxy.Requests.Accounts.GET.GetKidAccounts.Response;

namespace KidsWallet.API.Proxy.Tests.E2E.Accounts.GET;

internal sealed class GetAccounts : AccountsTestBase
{
    [Test]
    public async Task Should_NotThrow_OnHappyPath()
    {
        // Arrange
        await CreateAccount();
        
        // Act
        Func<Task<IReadOnlyCollection<GetKidAccountsResponse>>> act = async () =>
            await WebAppClient.AccountsApi.GetAccounts(_accountId);
        
        // Assert
        await act.Should().NotThrowAsync();
    }
}