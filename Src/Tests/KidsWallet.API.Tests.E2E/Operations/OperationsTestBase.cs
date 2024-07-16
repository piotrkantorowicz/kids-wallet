using Bogus.Extensions;

using KidsWallet.API.Proxy.Requests.Operations.POST.CreateKidAccountOperation.Request;
using KidsWallet.API.Tests.E2E.Accounts;

namespace KidsWallet.API.Tests.E2E.Operations;

internal abstract class OperationsTestBase : AccountsTestBase
{
    protected Guid _operationId;

    protected async Task CreateOperation()
    {
        // Arrange
        await CreateWallet();
        await CreateAccount();

        await WebApp.Host.Scenario(configure: x =>
        {
            _operationId = _faker.Random.Guid();
            string? title = _faker.Random.String2(length: 10);
            decimal amount = _faker.Random.Decimal2(min: -300, max: 300);
            DateTime dueDate = _faker.Date.Past().ToUniversalTime();

            CreateKidAccountOperationRequest_OperationType operationType = amount > 0
                ? CreateKidAccountOperationRequest_OperationType.Income
                : CreateKidAccountOperationRequest_OperationType.Expense;

            CreateKidAccountOperationRequest createRequest = new(KidAccountOperationId: _operationId,
                KidAccountId: _accountId, Amount: amount, Title: title, DueDate: dueDate, OperationType: operationType);

            // Act
            x.Post.Json(input: createRequest).ToUrl(url: "/v1/operations");

            // Assert
            x.StatusCodeShouldBe(statusCode: 200);
        });
    }
}