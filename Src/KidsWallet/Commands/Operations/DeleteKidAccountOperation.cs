using FluentValidation;

using KidsWallet.Persistence.Model.Operations;
using KidsWallet.Services.Abstraction;

using Wolverine.Attributes;

namespace KidsWallet.Commands.Operations;

public sealed record DeleteKidAccountOperationCommand(Guid KidAccountOperationId);

public sealed class DeleteKidAccountOperationCommandValidator : AbstractValidator<DeleteKidAccountOperationCommand>
{
    public DeleteKidAccountOperationCommandValidator()
    {
        RuleFor(x => x.KidAccountOperationId).NotEmpty();
    }
}

public static class DeleteKidAccountOperationCommandHandler
{
    [Transactional]
    public static async Task Handle(DeleteKidAccountOperationCommand command,
        ICrudOperationsService<KidAccountOperation> kidOperationCrudOperationsService,
        CancellationToken cancellationToken)
    {
        await kidOperationCrudOperationsService.DeleteAsync(command.KidAccountOperationId, cancellationToken);
    }
}