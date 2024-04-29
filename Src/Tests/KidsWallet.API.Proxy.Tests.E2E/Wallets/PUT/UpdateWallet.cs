using FluentAssertions;

using KidsWallet.API.Proxy.Requests.Wallets.PUT.UpdateKidWallet.Request;

namespace KidsWallet.API.Proxy.Tests.E2E.Wallets.PUT;

internal sealed class UpdateWallet : WalletsTestBase
{
    [Test]
    public async Task Should_NotThrow_OnHappyPath()
    {
        // Arrange
        await CreateWallet();
        UpdateKidWalletRequest updateKidWalletRequest = new(_faker.Random.String2(30));
        
        // Act
        Func<Task> act = async () => await WebAppClient.WalletsApi.UpdateWallet(_walletId, updateKidWalletRequest);
        
        // Assert
        await act.Should().NotThrowAsync();
    }
}