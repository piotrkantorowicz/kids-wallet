using NUnit.Framework;

namespace KidsWallet.Tests.E2E.Operations.Post;

internal sealed class AddOperation : OperationsTestBase
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