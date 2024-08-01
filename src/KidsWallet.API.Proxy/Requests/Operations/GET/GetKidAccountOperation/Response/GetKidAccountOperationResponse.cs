namespace KidsWallet.API.Proxy.Requests.Operations.GET.GetKidAccountOperation.Response;

public sealed record GetKidAccountOperationResponse(
    Guid KidAccountOperationId,
    Guid KidAccountId,
    string? Description,
    DateTime DueDate,
    decimal Amount,
    GetKidAccountOperationResponse_OperationType OperationType);

public enum GetKidAccountOperationResponse_OperationType
{
    None,
    Income,
    Expense,
    Transfer
}