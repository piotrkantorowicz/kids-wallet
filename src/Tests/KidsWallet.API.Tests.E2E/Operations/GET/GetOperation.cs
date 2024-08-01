using NUnit.Framework;

namespace KidsWallet.API.Tests.E2E.Operations.GET;

internal sealed class GetOperation : OperationsTestBase
{
    [Test]
    public async Task Should_Returns200_OnHappyPath()
    {
        // Arrange
        await CreateOperation();

        await WebApp.Host.Scenario(configure: x =>
        {
            // Act
            x.Get.Url(relativeUrl: $"/v1/operations/{_operationId}");

            // Assert
            x.StatusCodeShouldBe(statusCode: 200);
        });
    }
}