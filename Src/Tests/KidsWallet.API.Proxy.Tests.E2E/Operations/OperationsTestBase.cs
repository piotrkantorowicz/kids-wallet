using Bogus.Extensions;

using FluentAssertions;

using KidsWallet.API.Proxy.Requests.Operations.POST.CreateKidAccountOperation.Request;
using KidsWallet.API.Proxy.Tests.E2E.Accounts;

namespace KidsWallet.API.Proxy.Tests.E2E.Operations;

internal abstract class OperationsTestBase : AccountsTestBase
{
    protected Guid _operationId;

    protected async Task CreateOperation()
    {
        await CreateWallet();
        await CreateAccount();
        _operationId = _faker.Random.Guid();

        CreateKidAccountOperationRequest_OperationType operationType = _faker.Random.Decimal2(min: -500, max: 500) > 0
            ? CreateKidAccountOperationRequest_OperationType.Income
            : CreateKidAccountOperationRequest_OperationType.Expense;

        CreateKidAccountOperationRequest createKidAccountRequest = new(KidAccountOperationId: _operationId,
            KidAccountId: _accountId, Amount: _faker.Random.Decimal(), Title: _faker.Random.String2(length: 30),
            DueDate: _faker.Date.Past(), OperationType: operationType);

        // Act
        Func<Task> act = async () => await WebAppClient.OperationsApi.CreateOperation(model: createKidAccountRequest);

        // Assert
        await act.Should().NotThrowAsync();
    }
}