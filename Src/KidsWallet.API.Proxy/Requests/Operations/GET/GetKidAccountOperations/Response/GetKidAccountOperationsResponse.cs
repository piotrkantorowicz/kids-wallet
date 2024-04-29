namespace KidsWallet.API.Proxy.Requests.Operations.GET.GetKidAccountOperations.Response;

public sealed record GetKidAccountOperationsResponse(
    Guid Id,
    Guid KidAccountId,
    string? Description,
    DateTime DueDate,
    decimal Amount,
    GetKidAccountOperationsResponse_OperationType OperationType);

public enum GetKidAccountOperationsResponse_OperationType
{
    None,
    Income,
    Expense,
    Transfer
}