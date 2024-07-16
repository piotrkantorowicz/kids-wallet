using KidsWallet.API.Proxy.Requests.Wallets.GET.GetKidWallet.Response;
using KidsWallet.API.Proxy.Requests.Wallets.GET.GetKidWallets.Response;
using KidsWallet.API.Proxy.Requests.Wallets.POST.CreateKidWallet.Request;
using KidsWallet.API.Proxy.Requests.Wallets.PUT.UpdateKidWallet.Request;
using KidsWallet.Commands.Wallets;
using KidsWallet.Queries.Wallets;

using Microsoft.AspNetCore.Mvc;

using Wolverine;

namespace KidsWallet.API.Controllers;

[ApiController, Route(template: "v1/wallets")]
public sealed class WalletsController : ControllerBase
{
    private readonly IMessageBus _messageBus;

    public WalletsController(IMessageBus messageBus)
    {
        _messageBus = messageBus ?? throw new ArgumentNullException(paramName: nameof(messageBus));
    }

    [HttpGet(template: "{id:guid}")]
    public async Task<GetKidWalletResponse> GetWallet(Guid id, Guid? kidId, string? name, DateTime? createdAt,
        DateTime? updatedAt, bool? includeKidAccounts, bool? includeKidAccountOperations)
    {
        return await _messageBus.InvokeAsync<GetKidWalletResponse>(message: new GetKidWalletQuery(KidWalletId: id,
            KidId: kidId, Name: name, CreatedAt: createdAt, UpdatedAt: updatedAt,
            IncludeKidAccounts: includeKidAccounts, IncludeKidAccountOperations: includeKidAccountOperations));
    }

    [HttpGet]
    public async Task<IReadOnlyCollection<GetKidWalletsResponse>> GetWallets(Guid? id, Guid? kidId, string? name,
        DateTime? createdAt, DateTime? updatedAt, bool? includeKidAccounts, bool? includeKidAccountOperations)
    {
        return await _messageBus.InvokeAsync<IReadOnlyCollection<GetKidWalletsResponse>>(
            message: new GetKidWalletsQuery(KidWalletId: id, KidId: kidId, Name: name, CreatedAt: createdAt,
                UpdatedAt: updatedAt, IncludeKidAccounts: includeKidAccounts,
                IncludeKidAccountOperations: includeKidAccountOperations));
    }

    [HttpPost]
    public async Task PostWallet([FromBody] CreateKidWalletRequest request)
    {
        await _messageBus.InvokeAsync(message: new CreateKidWalletCommand(KidWalletId: request.KidWalletId,
            KidId: request.KidId, Name: request.Name));
    }

    [HttpPut(template: "{id:guid}")]
    public async Task PutWallet(Guid id, [FromBody] UpdateKidWalletRequest request)
    {
        await _messageBus.InvokeAsync(message: new UpdateKidWalletCommand(KidWalletId: id, Name: request.Name));
    }

    [HttpDelete(template: "{id:guid}")]
    public async Task DeleteWallet(Guid id)
    {
        await _messageBus.InvokeAsync(message: new DeleteKidWalletCommand(KidWalletId: id));
    }
}