using FluentValidation;

using KidsWallet.Persistence.Model.Accounts;
using KidsWallet.Persistence.Model.Wallets;
using KidsWallet.Services.Abstraction;

using Wolverine.Attributes;

namespace KidsWallet.Commands.Accounts;

public sealed record CreateKidAccountRequest(Guid AccountId, string Name, decimal Balance, Guid KidWalletId);

public sealed record CreateKidAccountCommand(Guid AccountId, string Name, decimal Balance, Guid KidWalletId);

public sealed class CreateKidAccountCommandValidator : AbstractValidator<CreateKidAccountCommand>
{
    public CreateKidAccountCommandValidator()
    {
        RuleFor(x => x.AccountId).NotEmpty();
        RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Balance);
        RuleFor(x => x.KidWalletId).NotEmpty();
    }
}

public static class CreateKidAccountCommandHandler
{
    [Transactional]
    public static async Task Handle(CreateKidAccountCommand command,
        ICrudOperationsService<KidAccount> kidAccountCrudOperationsService,
        ICrudOperationsService<KidWallet> kidWalletCrudOperationsService, CancellationToken cancellationToken)
    {
        KidWallet? kidWallet =
            await kidWalletCrudOperationsService.GetByIdAsync(command.KidWalletId, true, cancellationToken);
        
        await kidAccountCrudOperationsService.CreateAsync(command.KidWalletId, () => new KidAccount
        {
            Id = command.AccountId,
            Name = command.Name,
            KidWalletId = command.KidWalletId,
            KidWallet = kidWallet,
            Balance = command.Balance
        }, cancellationToken);
    }
}