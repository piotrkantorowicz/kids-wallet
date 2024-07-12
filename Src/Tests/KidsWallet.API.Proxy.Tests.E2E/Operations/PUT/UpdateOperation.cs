using KidsWallet.API.Proxy.Requests.Operations.PUT.UpdateKidAccountOperation.Request;

namespace KidsWallet.API.Proxy.Tests.E2E.Operations.PUT;

internal class UpdateOperation : OperationsTestBase
{
    [Test]
    public async Task Should_NotThrow_OnHappyPath()
    {
        // Arrange
        await CreateOperation();

        UpdateKidAccountOperationRequest updateKidAccountOperationRequest =
            new(Title: _faker.Random.String2(length: 50), Amount: _faker.Random.Decimal(), DueDate: _faker.Date.Past(),
                OperationType: _faker.Random.Enum<UpdateKidAccountOperationRequest_OperationType>());

        // Act
        Func<Task> act = async () =>
            await WebAppClient.OperationsApi.UpdateOperation(id: _operationId, model: updateKidAccountOperationRequest);

        // Assert
        await act.Invoke();
    }
}