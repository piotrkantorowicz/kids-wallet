﻿using FluentAssertions;

using KidsWallet.API.Proxy.Requests.Accounts.PUT.UpdateKidAccounts.Request;

namespace KidsWallet.API.Proxy.Tests.E2E.Accounts.PUT;

internal sealed class UpdateAccount : AccountsTestBase
{
    [Test]
    public async Task Should_NotThrow_OnHappyPath()
    {
        // Arrange
        await CreateAccount();
        UpdateKidAccountRequest updateKidAccountRequest = new(_faker.Random.String2(15), _faker.Random.Decimal());
        
        // Act
        Func<Task> act = async () => await WebAppClient.AccountsApi.UpdateAccount(_accountId, updateKidAccountRequest);
        
        // Assert
        await act.Should().NotThrowAsync();
    }
}