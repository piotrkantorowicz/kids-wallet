namespace KidsWallet.API.Proxy.Operations.GET.GetKidAccountOperation.Response;

public sealed record GetKidAccountOperationResponse(
    Guid Id,
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

