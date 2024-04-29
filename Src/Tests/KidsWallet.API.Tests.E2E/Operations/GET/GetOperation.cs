using NUnit.Framework;

namespace KidsWallet.Tests.E2E.Operations.GET;

internal sealed class GetOperation : OperationsTestBase
{
    [Test]
    public async Task Should_Returns200_OnHappyPath()
    {
        // Arrange
        await CreateOperation();
        
        await WebApp.Host.Scenario(x =>
        {
            // Act
            x.Get.Url($"/v1/operations/{_operationId}");
            
            // Assert
            x.StatusCodeShouldBe(200);
        });
    }
}