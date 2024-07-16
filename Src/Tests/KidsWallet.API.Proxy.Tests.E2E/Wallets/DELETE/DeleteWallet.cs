using FluentAssertions;

namespace KidsWallet.API.Proxy.Tests.E2E.Wallets.DELETE;

internal sealed class DeleteWallet : WalletsTestBase
{
    [Test]
    public async Task Should_NotThrow_OnHappyPath()
    {
        // Arrange
        await CreateWallet();

        // Act
        Func<Task> act = async () => await WebAppClient.WalletsApi.DeleteWallet(id: _walletId);

        // Assert
        await act.Should().NotThrowAsync();
    }
}