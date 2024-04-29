namespace KidsWallet.API.Proxy.Requests.Operations.POST.CreateKidAccountOperation.Request;


public sealed record CreateKidAccountOperationRequest(
    Guid OperationId,
    Guid AccountId,
    decimal Amount,
    string Title,
    DateTime DueDate,
    CreateKidAccountOperationRequest_OperationType OperationType);

public enum CreateKidAccountOperationRequest_OperationType
{
    None,
    Income,
    Expense,
    Transfer
}