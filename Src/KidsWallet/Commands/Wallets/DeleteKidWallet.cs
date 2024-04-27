using FluentValidation;

using KidsWallet.Persistence.Model.Wallets;
using KidsWallet.Services.Abstraction;

using Wolverine.Attributes;

namespace KidsWallet.Commands.Wallets;

public sealed record DeleteKidWalletCommand(Guid WalletId);

public sealed class DeleteKidWalletCommandValidator : AbstractValidator<DeleteKidWalletCommand>
{
    public DeleteKidWalletCommandValidator()
    {
        RuleFor(x => x.WalletId).NotEmpty();
    }
}

public static class DeleteKidWalletCommandHandler
{
    [Transactional]
    public static async Task Handle(DeleteKidWalletCommand command,
        ICrudOperationsService<KidWallet> kidWalletCrudOperationsService, CancellationToken cancellationToken)
    {
        await kidWalletCrudOperationsService.DeleteAsync(command.WalletId, cancellationToken);
    }
}