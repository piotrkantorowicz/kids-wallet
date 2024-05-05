using KidsWallet.API.Proxy.Requests.Wallets.GET.GetKidWallet.Response;
using KidsWallet.API.Proxy.Requests.Wallets.GET.GetKidWallets.Response;
using KidsWallet.API.Proxy.Requests.Wallets.POST.CreateKidWallet.Request;
using KidsWallet.API.Proxy.Requests.Wallets.PUT.UpdateKidWallet.Request;
using KidsWallet.Commands.Wallets;
using KidsWallet.Queries.Wallets;

using Microsoft.AspNetCore.Mvc;

using Wolverine;

namespace KidsWallet.API.Controllers;

[ApiController, Route("v1/wallets")]
public sealed class WalletsController : ControllerBase
{
    private readonly IMessageBus _messageBus;
    
    public WalletsController(IMessageBus messageBus)
    {
        _messageBus = messageBus ?? throw new ArgumentNullException(nameof(messageBus));
    }
    
    [HttpGet("{id:guid}")]
    public async Task<GetKidWalletResponse> GetWallet(Guid id, Guid? kidId, string? name, DateTime? createdAt,
        DateTime? updatedAt, bool? includeKidAccounts, bool? includeKidAccountOperations)
    {
        return await _messageBus.InvokeAsync<GetKidWalletResponse>(new GetKidWalletQuery(id, kidId, name, createdAt,
            updatedAt, includeKidAccounts, includeKidAccountOperations));
    }
    
    [HttpGet]
    public async Task<IReadOnlyCollection<GetKidWalletsResponse>> GetWallets(Guid? id, Guid? kidId, string? name,
        DateTime? createdAt, DateTime? updatedAt, bool? includeKidAccounts, bool? includeKidAccountOperations)
    {
        return await _messageBus.InvokeAsync<IReadOnlyCollection<GetKidWalletsResponse>>(new GetKidWalletsQuery(id,
            kidId, name, createdAt, updatedAt, includeKidAccounts, includeKidAccountOperations));
    }
    
    [HttpPost]
    public async Task PostWallet([FromBody] CreateKidWalletRequest request)
    {
        await _messageBus.InvokeAsync(new CreateKidWalletCommand(request.KidWalletId, request.KidId, request.Name));
    }
    
    [HttpPut("{id:guid}")]
    public async Task PutWallet(Guid id, [FromBody] UpdateKidWalletRequest request)
    {
        await _messageBus.InvokeAsync(new UpdateKidWalletCommand(id, request.Name));
    }
    
    [HttpDelete("{id:guid}")]
    public async Task DeleteWallet(Guid id)
    {
        await _messageBus.InvokeAsync(new DeleteKidWalletCommand(id));
    }
}