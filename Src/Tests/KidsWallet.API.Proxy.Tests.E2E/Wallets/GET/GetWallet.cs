using FluentAssertions;

using KidsWallet.API.Proxy.Requests.Wallets.GET.GetKidWallet.Response;

namespace KidsWallet.API.Proxy.Tests.E2E.Wallets.GET;

internal sealed class GetWallet : WalletsTestBase
{
    [Test]
    public async Task Should_NotThrow_OnHappyPath()
    {
        // Arrange
        await CreateWallet();
        
        // Act
        Func<Task<GetKidWalletResponse>> act = async () => await WebAppClient.WalletsApi.GetWallet(id: _walletId);
        
        // Assert
        await act.Should().NotThrowAsync();
    }
}