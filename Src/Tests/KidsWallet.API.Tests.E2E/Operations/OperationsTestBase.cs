using Bogus.Extensions;

using KidsWallet.API.Proxy.Requests.Operations.POST.CreateKidAccountOperation.Request;
using KidsWallet.Tests.E2E.Accounts;

namespace KidsWallet.Tests.E2E.Operations;

internal abstract class OperationsTestBase : AccountsTestBase
{
    protected Guid _operationId;
    
    protected async Task CreateOperation()
    {
        // Arrange
        await CreateWallet();
        await CreateAccount();
        
        await WebApp.Host.Scenario(x =>
        {
            _operationId = _faker.Random.Guid();
            string? title = _faker.Random.String2(10);
            decimal amount = _faker.Random.Decimal2(-300, 300);
            DateTime dueDate = _faker.Date.Past().ToUniversalTime();
            
            CreateKidAccountOperationRequest_OperationType operationType = amount > 0
                ? CreateKidAccountOperationRequest_OperationType.Income
                : CreateKidAccountOperationRequest_OperationType.Expense;
            
            CreateKidAccountOperationRequest createRequest =
                new(_operationId, _accountId, amount, title, dueDate, operationType);
            
            // Act
            x.Post.Json(createRequest).ToUrl("/v1/operations");
            
            // Assert
            x.StatusCodeShouldBe(200);
        });
    }
}