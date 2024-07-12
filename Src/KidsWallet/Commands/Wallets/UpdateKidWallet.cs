using FluentValidation;

using KidsWallet.Persistence.Model.Wallets;
using KidsWallet.Services.Abstraction;

using Wolverine.Attributes;

namespace KidsWallet.Commands.Wallets;

public sealed record UpdateKidWalletCommand(Guid KidWalletId, string Name);

public sealed class UpdateKidWalletCommandValidator : AbstractValidator<UpdateKidWalletCommand>
{
    public UpdateKidWalletCommandValidator()
    {
        RuleFor(expression: x => x.KidWalletId).NotEmpty();
        RuleFor(expression: x => x.Name).NotEmpty().Length(min: 5, max: 150);
    }
}

public static class UpdateKidWalletCommandHandler
{
    [Transactional]
    public static async Task Handle(UpdateKidWalletCommand command,
        ICrudOperationsService<KidWallet> kidWalletCrudOperationsService, CancellationToken cancellationToken)
    {
        await kidWalletCrudOperationsService.UpdateAsync(id: command.KidWalletId, updateEntityFunc: dbEntity =>
        {
            dbEntity.Name = command.Name;

            return dbEntity;
        }, cancellationToken: cancellationToken);
    }
}