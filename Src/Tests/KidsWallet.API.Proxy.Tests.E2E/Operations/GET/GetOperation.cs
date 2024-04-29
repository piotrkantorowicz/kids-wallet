using FluentAssertions;

namespace KidsWallet.API.Proxy.Tests.E2E.Operations.GET;

internal sealed class GetOperation : OperationsTestBase
{
    [Test]
    public async Task Should_NotThrow_OnHappyPath()
    {
        // Arrange
        await CreateOperation();
        
        // Act
        Func<Task> act = async () => await WebAppClient.OperationsApi.GetOperation(id: _operationId);
        
        // Assert
        await act.Should().NotThrowAsync();
    }
}