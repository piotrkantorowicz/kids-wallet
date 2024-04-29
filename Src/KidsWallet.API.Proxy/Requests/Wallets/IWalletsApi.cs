﻿using KidsWallet.API.Proxy.Requests.Wallets.GET.GetKidWallet.Response;
using KidsWallet.API.Proxy.Requests.Wallets.GET.GetKidWallets.Response;
using KidsWallet.API.Proxy.Requests.Wallets.POST.CreateKidWallet.Request;
using KidsWallet.API.Proxy.Requests.Wallets.PUT.UpdateKidWallet.Request;

using RestEase;

namespace KidsWallet.API.Proxy.Requests.Wallets;

[BasePath("v1/wallets"), Header("Cache-Control", "no-cache")]
public interface IWalletsApi
{
    [Get("{id}")]
    Task<GetKidWalletResponse> GetWallet([Path] Guid id, [Query] Guid? kidId = null, [Query] string? name = null,
        [Query] DateTime? createdAt = null, [Query] DateTime? updatedAt = null, [Query] bool? includeKidAccounts = null,
        [Query] bool? includeKidAccountOperations = null);
    
    [Get]
    Task<IReadOnlyCollection<GetKidWalletsResponse>> GetWallets([Query] Guid? id = null, [Query] Guid? kidId = null,
        [Query] string? name = null, [Query] DateTime? createdAt = null, [Query] DateTime? updatedAt = null,
        [Query] bool? includeKidAccounts = null, [Query] bool? includeKidAccountOperations = null);
    
    [Post]
    Task CreateWallet([Body] CreateKidWalletRequest model);
    
    [Put("{id}")]
    Task UpdateWallet([Path] Guid id, [Body] UpdateKidWalletRequest model);
    
    [Delete("{id}")]
    Task DeleteWallet([Path] Guid id);
}