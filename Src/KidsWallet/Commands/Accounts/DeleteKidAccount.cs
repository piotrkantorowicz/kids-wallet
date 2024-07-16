using FluentValidation;

using KidsWallet.Persistence.Model.Accounts;
using KidsWallet.Services.Abstraction;

using Wolverine.Attributes;

namespace KidsWallet.Commands.Accounts;

public sealed record DeleteKidAccountCommand(Guid KidAccountId);

public sealed class DeleteKidAccountCommandValidator : AbstractValidator<DeleteKidAccountCommand>
{
    public DeleteKidAccountCommandValidator()
    {
        RuleFor(expression: x => x.KidAccountId).NotEmpty();
    }
}

public static class DeleteKidAccountCommandHandler
{
    [Transactional]
    public static async Task Handle(DeleteKidAccountCommand command,
        ICrudOperationsService<KidAccount> kidAccountCrudOperationsService, CancellationToken cancellationToken)
    {
        await kidAccountCrudOperationsService.DeleteAsync(id: command.KidAccountId,
            cancellationToken: cancellationToken);
    }
}