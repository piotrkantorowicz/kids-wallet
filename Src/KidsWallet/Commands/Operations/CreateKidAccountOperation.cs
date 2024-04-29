using FluentValidation;

using KidsWallet.Persistence.Model.Accounts;
using KidsWallet.Persistence.Model.Operations;
using KidsWallet.Services.Abstraction;

using Wolverine.Attributes;

namespace KidsWallet.Commands.Operations;

public sealed record CreateKidAccountOperationCommand(
    Guid KidAccountOperationId,
    Guid KidAccountId,
    decimal Amount,
    string Title,
    DateTime DueDate,
    OperationType OperationType);

public sealed class CreateKidAccountOperationCommandValidator : AbstractValidator<CreateKidAccountOperationCommand>
{
    public CreateKidAccountOperationCommandValidator()
    {
        RuleFor(x => x.KidAccountOperationId).NotEmpty();
        RuleFor(x => x.KidAccountId).NotEmpty();
        RuleFor(x => x.Amount);
        RuleFor(x => x.Title).NotEmpty().Length(3, 200);
        RuleFor(x => x.DueDate).NotEmpty();
        RuleFor(x => x.OperationType).NotEmpty();
    }
}

public static class CreateKidAccountOperationCommandHandler
{
    [Transactional]
    public static async Task Handle(CreateKidAccountOperationCommand command,
        ICrudOperationsService<KidAccountOperation> kidAccountOperationCrudOperationsService,
        ICrudOperationsService<KidAccount> kidAccountCrudOperationsService, CancellationToken cancellationToken)
    {
        KidAccount? kidAccount =
            await kidAccountCrudOperationsService.GetByIdAsync(command.KidAccountId, true, cancellationToken);
        
        await kidAccountOperationCrudOperationsService.CreateAsync(command.KidAccountId, () => new KidAccountOperation
        {
            Id = command.KidAccountOperationId,
            KidAccountId = command.KidAccountId,
            KidAccount = kidAccount,
            Amount = command.Amount,
            Description = command.Title,
            DueDate = command.DueDate,
            CreatedAt = DateTime.UtcNow,
            OperationType = command.OperationType
        }, cancellationToken);
    }
}