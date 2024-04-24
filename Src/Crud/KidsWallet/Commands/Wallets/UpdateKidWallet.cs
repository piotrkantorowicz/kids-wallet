using FluentValidation;

using KidsWallet.Persistence.Model.Wallets;
using KidsWallet.Services.Abstraction;

using Wolverine.Attributes;

namespace KidsWallet.Commands.Wallets;

public sealed record UpdateKidWalletRequest(string Name);

public sealed record UpdateKidWalletCommand(Guid WalletId, string Name);

public sealed class UpdateKidWalletCommandValidator : AbstractValidator<UpdateKidWalletCommand>
{
    public UpdateKidWalletCommandValidator()
    {
        RuleFor(x => x.WalletId).NotEmpty();
        RuleFor(x => x.Name).NotEmpty().Length(5, 150);
    }
}

public static class UpdateKidWalletCommandHandler
{
    [Transactional]
    public static async Task Handle(UpdateKidWalletCommand command,
        ICrudOperationsService<KidWallet> kidWalletCrudOperationsService, CancellationToken cancellationToken)
    {
        await kidWalletCrudOperationsService.UpdateAsync(command.WalletId, dbEntity =>
        {
            dbEntity.Name = command.Name;
            
            return dbEntity;
        }, cancellationToken);
    }
}