using FluentValidation;

using KidsWallet.Persistence.Model.Accounts;
using KidsWallet.Persistence.Model.Wallets;
using KidsWallet.Services.Abstraction;

using Wolverine.Attributes;

namespace KidsWallet.Commands.Accounts;

public sealed record CreateKidAccountCommand(Guid KidAccountId, Guid KidWalletId, string Name, decimal Balance);

public sealed class CreateKidAccountCommandValidator : AbstractValidator<CreateKidAccountCommand>
{
    public CreateKidAccountCommandValidator()
    {
        RuleFor(expression: x => x.KidAccountId).NotEmpty();
        RuleFor(expression: x => x.Name).NotEmpty().MaximumLength(maximumLength: 100);
        RuleFor(expression: x => x.Balance).GreaterThanOrEqualTo(valueToCompare: 0);
        RuleFor(expression: x => x.KidWalletId).NotEmpty();
    }
}

public static class CreateKidAccountCommandHandler
{
    [Transactional]
    public static async Task Handle(CreateKidAccountCommand command,
        ICrudOperationsService<KidAccount> kidAccountCrudOperationsService,
        ICrudOperationsService<KidWallet> kidWalletCrudOperationsService, CancellationToken cancellationToken)
    {
        KidWallet? kidWallet = await kidWalletCrudOperationsService.GetByIdAsync(id: command.KidWalletId,
            throwWhenNotFound: true, cancellationToken: cancellationToken);

        await kidAccountCrudOperationsService.CreateAsync(id: command.KidWalletId, createEntityFunc: () =>
            new KidAccount
            {
                Id = command.KidAccountId,
                Name = command.Name,
                KidWalletId = command.KidWalletId,
                KidWallet = kidWallet,
                Balance = command.Balance
            }, cancellationToken: cancellationToken);
    }
}