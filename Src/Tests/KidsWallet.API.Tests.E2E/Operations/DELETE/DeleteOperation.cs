using NUnit.Framework;

namespace KidsWallet.API.Tests.E2E.Operations.DELETE;

internal sealed class DeleteOperation : OperationsTestBase
{
    [Test]
    public async Task Should_Returns200_OnHappyPath()
    {
        // Arrange
        await CreateOperation();
        
        await WebApp.Host.Scenario(x =>
        {
            // Act
            x.Delete.Url($"/v1/operations/{_operationId}");
            
            // Assert
            x.StatusCodeShouldBe(200);
        });
    }
}