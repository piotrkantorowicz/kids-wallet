using FluentValidation;

using KidsWallet.Persistence.Model.Wallets;
using KidsWallet.Services.Abstraction;

using Wolverine.Attributes;

namespace KidsWallet.Commands.Wallets;

public sealed record CreateKidWalletCommand(Guid WalletId, Guid KidId, string Name);

public sealed class CreateKidWalletCommandValidator : AbstractValidator<CreateKidWalletCommand>
{
    public CreateKidWalletCommandValidator()
    {
        RuleFor(x => x.WalletId).NotEmpty();
        RuleFor(x => x.KidId).NotEmpty();
        RuleFor(x => x.Name).NotEmpty().Length(5, 150);
    }
}

public static class CreateKidWalletCommandHandler
{
    [Transactional]
    public static async Task Handle(CreateKidWalletCommand command,
        ICrudOperationsService<KidWallet> kidWalletCrudOperationsService, CancellationToken cancellationToken)
    {
        await kidWalletCrudOperationsService.CreateAsync(command.WalletId, () => new KidWallet
        {
            Id = command.WalletId,
            KidId = command.KidId,
            Name = command.Name
        }, cancellationToken);
    }
}