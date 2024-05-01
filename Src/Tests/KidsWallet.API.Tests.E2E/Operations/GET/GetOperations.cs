using NUnit.Framework;

namespace KidsWallet.API.Tests.E2E.Operations.GET;

internal sealed class GetOperations : OperationsTestBase
{
    [Test]
    public async Task Should_Returns200_OnHappyPath()
    {
        // Arrange
        await CreateOperation();
        
        await WebApp.Host.Scenario(x =>
        {
            // Act
            x.Get.Url($"/v1/operations?id={_accountId}");
            
            // Assert
            x.StatusCodeShouldBe(200);
        });
    }
}