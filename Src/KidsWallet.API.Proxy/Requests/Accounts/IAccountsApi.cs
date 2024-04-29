using KidsWallet.API.Proxy.Requests.Accounts.GET.GetKidAccount.Response;
using KidsWallet.API.Proxy.Requests.Accounts.GET.GetKidAccounts.Response;
using KidsWallet.API.Proxy.Requests.Accounts.POST.CreateKidAccount.Request;
using KidsWallet.API.Proxy.Requests.Accounts.PUT.UpdateKidAccounts.Request;

using RestEase;

namespace KidsWallet.API.Proxy.Requests.Accounts;

[BasePath("v1/accounts"), Header("Cache-Control", "no-cache")]
public interface IAccountsApi
{
    [Get("{id}")]
    Task<GetKidAccountResponse> GetAccount([Path] Guid id, [Query] Guid? kidWalletId = null,
        [Query] string? name = null, [Query] decimal? balance = null, [Query] DateTime? updatedAt = null,
        [Query] DateTime? createdAt = null, [Query] bool? includeKidAccountOperations = null);
    
    [Get]
    Task<IReadOnlyCollection<GetKidAccountsResponse>> GetAccounts([Query] Guid? id = null,
        [Query] Guid? kidWalletId = null, [Query] string? name = null, [Query] decimal? balance = null,
        [Query] DateTime? updatedAt = null, [Query] DateTime? createdAt = null,
        [Query] bool? includeKidAccountOperations = null);
    
    [Post]
    Task CreateAccount([Body] CreateKidAccountRequest model);
    
    [Put("{id}")]
    Task UpdateAccount([Path] Guid id, [Body] UpdateKidAccountRequest model);
    
    [Delete("{id}")]
    Task DeleteAccount([Path] Guid id);
}