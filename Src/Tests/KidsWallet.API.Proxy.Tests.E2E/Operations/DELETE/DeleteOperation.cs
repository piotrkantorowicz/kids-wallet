using FluentAssertions;

namespace KidsWallet.API.Proxy.Tests.E2E.Operations.DELETE;

internal sealed class DeleteOperation : OperationsTestBase
{
    [Test]
    public async Task Should_NotThrow_OnHappyPath()
    {
        // Arrange
        await CreateOperation();
        
        // Act
        Func<Task> act = async () => await WebAppClient.OperationsApi.DeleteOperation(_operationId);
        
        // Assert
        await act.Should().NotThrowAsync();
    }
}