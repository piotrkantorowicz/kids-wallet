namespace KidsWallet.API.Proxy.Operations.PUT.UpdateKidAccountOperation.Request;

public sealed record UpdateKidAccountOperationRequest(
    decimal Amount,
    string Title,
    DateTime DueDate,
    UpdateKidAccountOperationRequest_OperationType OperationType);

public enum UpdateKidAccountOperationRequest_OperationType
{
    None,
    Income,
    Expense,
    Transfer
}