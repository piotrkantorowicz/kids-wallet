using FluentAssertions;

using KidsWallet.API.Proxy.Requests.Wallets.GET.GetKidWallets.Response;

namespace KidsWallet.API.Proxy.Tests.E2E.Wallets.GET;

internal sealed class GetWallets : WalletsTestBase
{
    [Test]
    public async Task Should_NotThrow_OnHappyPath()
    {
        // Arrange
        await CreateWallet();
        
        // Act
        Func<Task<IReadOnlyCollection<GetKidWalletsResponse>>> act = async () =>
            await WebAppClient.WalletsApi.GetWallets(id: _walletId);
        
        // Assert
        await act.Should().NotThrowAsync();
    }
}