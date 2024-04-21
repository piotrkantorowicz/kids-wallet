﻿using Bogus.Extensions;

using KidsWallet.Commands.Operations;

namespace KidsWallet.Tests.E2E.Operations.Put;

internal class UpdateOperation : OperationsTestBase
{
    [Test]
    public async Task Should_Returns200_OnHappyPath()
    {
        // Arrange
        await CreateOperation();
        
        await WebApp.Host.Scenario(x =>
        {
            string? newTitle = _faker.Random.String2(10);
            decimal newAmount = _faker.Random.Decimal2(-300, 300);
            DateTime newDueDate = _faker.Date.Past().ToUniversalTime();
            
            UpdateKidAccountOperationRequest_OperationType newOperationType = newAmount > 0
                ? UpdateKidAccountOperationRequest_OperationType.Income
                : UpdateKidAccountOperationRequest_OperationType.Expense;
            
            UpdateKidAccountOperationRequest updateRequest = new(newAmount, newTitle, newDueDate, newOperationType);
            
            // Act
            x.Put.Json(updateRequest).ToUrl($"/operations/{_operationId}");
            
            // Assert
            x.StatusCodeShouldBe(200);
        });
    }
}