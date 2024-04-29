using NUnit.Framework;

namespace KidsWallet.Tests.E2E.Operations.POST;

internal sealed class CreateOperation : OperationsTestBase
{
    [Test]
    public async Task Should_Returns200_OnHappyPath()
    {
        // Arrange
        // Act
        // Assert
        await CreateOperation();
    }
}