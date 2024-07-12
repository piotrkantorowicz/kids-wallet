using KidsWallet.API.Proxy.Requests.Operations.GET.GetKidAccountOperation.Response;
using KidsWallet.API.Proxy.Requests.Operations.GET.GetKidAccountOperations.Response;
using KidsWallet.API.Proxy.Requests.Operations.POST.CreateKidAccountOperation.Request;
using KidsWallet.API.Proxy.Requests.Operations.PUT.UpdateKidAccountOperation.Request;

using RestEase;

namespace KidsWallet.API.Proxy.Requests.Operations;

[BasePath(basePath: "v1/operations"), Header(name: "Cache-Control", value: "no-cache")]
public interface IOperationsApi
{
    [Get(path: "{id}")]
    Task<GetKidAccountOperationResponse> GetOperation([Path] Guid id, [Query] Guid? kidAccountId = null,
        [Query] string? description = null, [Query] decimal? amount = null, [Query] DateTime? dueDate = null,
        [Query] DateTime? updatedAt = null, [Query] DateTime? createdAt = null);

    [Get]
    Task<IReadOnlyCollection<GetKidAccountOperationsResponse>> GetOperations([Query] Guid? id = null,
        [Query] Guid? kidAccountId = null, [Query] string? description = null, [Query] decimal? amount = null,
        [Query] DateTime? dueDate = null, [Query] DateTime? updatedAt = null, [Query] DateTime? createdAt = null);

    [Post]
    Task CreateOperation([Body] CreateKidAccountOperationRequest model);

    [Put(path: "{id}")]
    Task UpdateOperation([Path] Guid id, [Body] UpdateKidAccountOperationRequest model);

    [Delete(path: "{id}")]
    Task DeleteOperation([Path] Guid id);
}