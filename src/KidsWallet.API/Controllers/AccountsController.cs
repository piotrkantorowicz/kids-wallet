using KidsWallet.API.Proxy.Requests.Accounts.GET.GetKidAccount.Response;
using KidsWallet.API.Proxy.Requests.Accounts.GET.GetKidAccounts.Response;
using KidsWallet.API.Proxy.Requests.Accounts.POST.CreateKidAccount.Request;
using KidsWallet.API.Proxy.Requests.Accounts.PUT.UpdateKidAccounts.Request;
using KidsWallet.Commands.Accounts;
using KidsWallet.Queries.Accounts;

using Microsoft.AspNetCore.Mvc;

using Wolverine;

namespace KidsWallet.API.Controllers;

[ApiController, Route(template: "v1/accounts")]
public class AccountsController : ControllerBase
{
    private readonly IMessageBus _messageBus;

    public AccountsController(IMessageBus messageBus)
    {
        _messageBus = messageBus ?? throw new ArgumentNullException(paramName: nameof(messageBus));
    }

    [HttpGet(template: "{id:guid}")]
    public async Task<GetKidAccountResponse> GetAccount(Guid id, Guid? kidWalletId, string? name, decimal? balance,
        DateTime? updatedAt, DateTime? createdAt, bool? includeKidAccountOperations)
    {
        return await _messageBus.InvokeAsync<GetKidAccountResponse>(message: new GetKidAccountQuery(KidAccountId: id,
            KidWalletId: kidWalletId, Name: name, Balance: balance, CreatedAt: createdAt, UpdatedAt: updatedAt,
            IncludeKidAccountOperations: includeKidAccountOperations));
    }

    [HttpGet]
    public async Task<IReadOnlyCollection<GetKidAccountsResponse>> GetAccounts(Guid? id, Guid? kidWalletId,
        string? name, decimal? balance, DateTime? updatedAt, DateTime? createdAt, bool? includeKidAccountOperations)
    {
        return await _messageBus.InvokeAsync<IReadOnlyCollection<GetKidAccountsResponse>>(
            message: new GetKidAccountsQuery(KidAccountId: id, KidWalletId: kidWalletId, Name: name, Balance: balance,
                CreatedAt: createdAt, UpdatedAt: updatedAt, IncludeKidAccountOperations: includeKidAccountOperations));
    }

    [HttpPost]
    public async Task PostAccount([FromBody] CreateKidAccountRequest request)
    {
        await _messageBus.InvokeAsync(message: new CreateKidAccountCommand(KidAccountId: request.KidAccountId,
            KidWalletId: request.KidWalletId, Name: request.Name, Balance: request.Balance));
    }

    [HttpPut(template: "{id:guid}")]
    public async Task PutAccount(Guid id, [FromBody] UpdateKidAccountRequest request)
    {
        await _messageBus.InvokeAsync(message: new UpdateKidAccountCommand(KidAccountId: id, Name: request.Name,
            Balance: request.Balance));
    }

    [HttpDelete(template: "{id:guid}")]
    public async Task DeleteAccount(Guid id)
    {
        await _messageBus.InvokeAsync(message: new DeleteKidAccountCommand(KidAccountId: id));
    }
}