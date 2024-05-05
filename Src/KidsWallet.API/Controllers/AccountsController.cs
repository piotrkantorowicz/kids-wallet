using KidsWallet.API.Proxy.Requests.Accounts.GET.GetKidAccount.Response;
using KidsWallet.API.Proxy.Requests.Accounts.GET.GetKidAccounts.Response;
using KidsWallet.API.Proxy.Requests.Accounts.POST.CreateKidAccount.Request;
using KidsWallet.API.Proxy.Requests.Accounts.PUT.UpdateKidAccounts.Request;
using KidsWallet.Commands.Accounts;
using KidsWallet.Queries.Accounts;

using Microsoft.AspNetCore.Mvc;

using Wolverine;

namespace KidsWallet.API.Controllers;

[ApiController, Route("v1/accounts")]
public class AccountsController : ControllerBase
{
    private readonly IMessageBus _messageBus;
    
    public AccountsController(IMessageBus messageBus)
    {
        _messageBus = messageBus ?? throw new ArgumentNullException(nameof(messageBus));
    }
    
    [HttpGet("{id:guid}")]
    public async Task<GetKidAccountResponse> GetAccount(Guid id, Guid? kidWalletId, string? name, decimal? balance,
        DateTime? updatedAt, DateTime? createdAt, bool? includeKidAccountOperations)
    {
        return await _messageBus.InvokeAsync<GetKidAccountResponse>(new GetKidAccountQuery(id, kidWalletId, name,
            balance, createdAt, updatedAt, includeKidAccountOperations));
    }
    
    [HttpGet]
    public async Task<IReadOnlyCollection<GetKidAccountsResponse>> GetAccounts(Guid? id, Guid? kidWalletId,
        string? name, decimal? balance, DateTime? updatedAt, DateTime? createdAt, bool? includeKidAccountOperations)
    {
        return await _messageBus.InvokeAsync<IReadOnlyCollection<GetKidAccountsResponse>>(new GetKidAccountsQuery(id,
            kidWalletId, name, balance, createdAt, updatedAt, includeKidAccountOperations));
    }
    
    [HttpPost]
    public async Task PostAccount([FromBody] CreateKidAccountRequest request)
    {
        await _messageBus.InvokeAsync(new CreateKidAccountCommand(WalletId: request.WalletId,
            KidAccountId: request.KidAccountId, Name: request.Name, Balance: request.Balance));
    }
    
    [HttpPut("{id:guid}")]
    public async Task PutAccount(Guid id, [FromBody] UpdateKidAccountRequest request)
    {
        await _messageBus.InvokeAsync(new UpdateKidAccountCommand(id, request.Name, request.Balance));
    }
    
    [HttpDelete("{id:guid}")]
    public async Task DeleteAccount(Guid id)
    {
        await _messageBus.InvokeAsync(new DeleteKidAccountCommand(id));
    }
}