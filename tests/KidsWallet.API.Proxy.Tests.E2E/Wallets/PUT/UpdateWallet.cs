﻿using FluentAssertions;

using KidsWallet.API.Proxy.Requests.Wallets.PUT.UpdateKidWallet.Request;

namespace KidsWallet.API.Proxy.Tests.E2E.Wallets.PUT;

internal sealed class UpdateWallet : WalletsTestBase
{
    [Test]
    public async Task Should_NotThrow_OnHappyPath()
    {
        // Arrange
        await CreateWallet();
        UpdateKidWalletRequest updateKidWalletRequest = new(Name: _faker.Random.String2(length: 30));

        // Act
        Func<Task> act = async () =>
            await WebAppClient.WalletsApi.UpdateWallet(id: _walletId, model: updateKidWalletRequest);

        // Assert
        await act.Should().NotThrowAsync();
    }
}