﻿using FluentValidation;

using KidsWallet.Persistence.Model.Accounts;
using KidsWallet.Persistence.Model.Wallets;
using KidsWallet.Services.Abstraction;

using Wolverine.Attributes;

namespace KidsWallet.Commands.Accounts;

public sealed record CreateKidAccountCommand(Guid KidAccountId, Guid WalletId, string Name, decimal Balance);

public sealed class CreateKidAccountCommandValidator : AbstractValidator<CreateKidAccountCommand>
{
    public CreateKidAccountCommandValidator()
    {
        RuleFor(x => x.KidAccountId).NotEmpty();
        RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Balance).GreaterThanOrEqualTo(0);
        RuleFor(x => x.WalletId).NotEmpty();
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
            await kidWalletCrudOperationsService.GetByIdAsync(command.WalletId, true, cancellationToken);
        
        await kidAccountCrudOperationsService.CreateAsync(command.WalletId, () => new KidAccount
        {
            Id = command.KidAccountId,
            Name = command.Name,
            KidWalletId = command.WalletId,
            KidWallet = kidWallet,
            Balance = command.Balance
        }, cancellationToken);
    }
}