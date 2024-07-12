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
        RuleFor(expression: x => x.KidAccountId).NotEmpty();
        RuleFor(expression: x => x.Name).NotEmpty().MaximumLength(maximumLength: 50);
        RuleFor(expression: x => x.Balance).GreaterThanOrEqualTo(valueToCompare: 0);
    }
}

public static class UpdateKidAccountCommandHandler
{
    [Transactional]
    public static async Task Handle(UpdateKidAccountCommand command,
        ICrudOperationsService<KidAccount> kidAccountCrudOperationsService, CancellationToken cancellationToken)
    {
        await kidAccountCrudOperationsService.UpdateAsync(id: command.KidAccountId, updateEntityFunc: dbEntity =>
        {
            dbEntity.Name = command.Name;
            dbEntity.Balance = command.Balance;

            return dbEntity;
        }, cancellationToken: cancellationToken);
    }
}