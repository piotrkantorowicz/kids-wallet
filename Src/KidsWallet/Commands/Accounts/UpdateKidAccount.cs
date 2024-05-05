using FluentValidation;

using KidsWallet.Persistence.Model.Accounts;
using KidsWallet.Services.Abstraction;

using Wolverine.Attributes;

namespace KidsWallet.Commands.Accounts;

public sealed record UpdateKidAccountCommand(Guid KidAccountId, string Name, decimal Balance);

public sealed class UpdateKidAccountCommandValidator : AbstractValidator<UpdateKidAccountCommand>
{
    public UpdateKidAccountCommandValidator()
    {
        RuleFor(x => x.KidAccountId).NotEmpty();
        RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
        RuleFor(x => x.Balance).GreaterThanOrEqualTo(0);
    }
}

public static class UpdateKidAccountCommandHandler
{
    [Transactional]
    public static async Task Handle(UpdateKidAccountCommand command,
        ICrudOperationsService<KidAccount> kidAccountCrudOperationsService, CancellationToken cancellationToken)
    {
        await kidAccountCrudOperationsService.UpdateAsync(command.KidAccountId, dbEntity =>
        {
            dbEntity.Name = command.Name;
            dbEntity.Balance = command.Balance;
            
            return dbEntity;
        }, cancellationToken);
    }
}