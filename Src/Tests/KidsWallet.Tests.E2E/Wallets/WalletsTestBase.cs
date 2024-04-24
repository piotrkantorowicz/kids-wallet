﻿using Bogus;

using KidsWallet.Commands.Wallets;

namespace KidsWallet.Tests.E2E.Wallets;

internal abstract class WalletsTestBase
{
    protected readonly Faker _faker = new();
    protected Guid _walletId;
    
    protected async Task CreateWallet()
    {
        await WebApp.Host.Scenario(x =>
        {
            // Arrange
            _walletId = _faker.Random.Guid();
            Guid kidId = _faker.Random.Guid();
            string? name = _faker.Random.String2(10);
            CreateKidWalletRequest createRequest = new(_walletId, kidId, name);
            
            // Act
            x.Post.Json(createRequest).ToUrl("/wallets");
            
            // Assert
            x.StatusCodeShouldBe(200);
        });
    }
}