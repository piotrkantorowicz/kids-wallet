using FluentValidation;

using KidsWallet.Persistence.Model.Accounts;
using KidsWallet.Services.Abstraction;

using Wolverine.Attributes;

namespace KidsWallet.Commands.Accounts;

public sealed record DeleteKidAccountCommand(Guid AccountId);

public sealed class DeleteKidAccountCommandValidator : AbstractValidator<DeleteKidAccountCommand>
{
    public DeleteKidAccountCommandValidator()
    {
        RuleFor(x => x.AccountId).NotEmpty();
    }
}

public static class DeleteKidAccountCommandHandler
{
    [Transactional]
    public static async Task Handle(DeleteKidAccountCommand command,
        ICrudOperationsService<KidAccount> kidAccountCrudOperationsService, CancellationToken cancellationToken)
    {
        await kidAccountCrudOperationsService.DeleteAsync(command.AccountId, cancellationToken);
    }
}