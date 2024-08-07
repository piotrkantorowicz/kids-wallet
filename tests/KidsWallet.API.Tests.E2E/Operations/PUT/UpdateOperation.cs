﻿using Bogus.Extensions;

using KidsWallet.API.Proxy.Requests.Operations.PUT.UpdateKidAccountOperation.Request;

using NUnit.Framework;

namespace KidsWallet.API.Tests.E2E.Operations.PUT;

internal class UpdateOperation : OperationsTestBase
{
    [Test]
    public async Task Should_Returns200_OnHappyPath()
    {
        // Arrange
        await CreateOperation();

        await WebApp.Host.Scenario(configure: x =>
        {
            string? newTitle = _faker.Random.String2(length: 10);
            decimal newAmount = _faker.Random.Decimal2(min: -300, max: 300);
            DateTime newDueDate = _faker.Date.Past().ToUniversalTime();

            UpdateKidAccountOperationRequest_OperationType newOperationType = newAmount > 0
                ? UpdateKidAccountOperationRequest_OperationType.Income
                : UpdateKidAccountOperationRequest_OperationType.Expense;

            UpdateKidAccountOperationRequest updateRequest = new(Amount: newAmount, Title: newTitle,
                DueDate: newDueDate, OperationType: newOperationType);

            // Act
            x.Put.Json(input: updateRequest).ToUrl(url: $"/v1/operations/{_operationId}");

            // Assert
            x.StatusCodeShouldBe(statusCode: 200);
        });
    }
}