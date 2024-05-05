using FluentValidation;

using KidsWallet.Persistence.Model.Wallets;
using KidsWallet.Services.Abstraction;

using Wolverine.Attributes;

namespace KidsWallet.Commands.Wallets;

public sealed record DeleteKidWalletCommand(Guid KidWalletId);

public sealed class DeleteKidWalletCommandValidator : AbstractValidator<DeleteKidWalletCommand>
{
    public DeleteKidWalletCommandValidator()
    {
        RuleFor(x => x.KidWalletId).NotEmpty();
    }
}

public static class DeleteKidWalletCommandHandler
{
    [Transactional]
    public static async Task Handle(DeleteKidWalletCommand command,
        ICrudOperationsService<KidWallet> kidWalletCrudOperationsService, CancellationToken cancellationToken)
    {
        await kidWalletCrudOperationsService.DeleteAsync(command.KidWalletId, cancellationToken);
    }
}