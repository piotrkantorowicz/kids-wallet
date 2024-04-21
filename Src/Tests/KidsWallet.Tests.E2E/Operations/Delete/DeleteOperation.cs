namespace KidsWallet.Tests.E2E.Operations.Delete;

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
            x.Delete.Url($"/operations/{_operationId}");
            
            // Assert
            x.StatusCodeShouldBe(200);
        });
    }
}