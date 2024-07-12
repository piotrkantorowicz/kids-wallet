using FluentValidation;

using KidsWallet.Persistence.Model.Operations;
using KidsWallet.Services.Abstraction;
using KidsWallet.Shared.Abstraction;

using Wolverine.Attributes;

namespace KidsWallet.Commands.Operations;

public sealed record UpdateKidAccountOperationCommand(
    Guid KidAccountOperationId,
    decimal Amount,
    string Title,
    DateTime DueDate,
    OperationType OperationType);

public sealed class UpdateKidAccountOperationCommandValidator : AbstractValidator<UpdateKidAccountOperationCommand>
{
    public UpdateKidAccountOperationCommandValidator()
    {
        RuleFor(expression: x => x.KidAccountOperationId).NotEmpty();
        RuleFor(expression: x => x.Amount);
        RuleFor(expression: x => x.Title).NotEmpty().Length(min: 3, max: 200);
        RuleFor(expression: x => x.DueDate).NotEmpty();
    }
}

public static class UpdateKidAccountOperationCommandHandler
{
    [Transactional]
    public static async Task Handle(UpdateKidAccountOperationCommand command,
        ICrudOperationsService<KidAccountOperation> kidOperationCrudOperationsService, IClock clock,
        CancellationToken cancellationToken)
    {
        await kidOperationCrudOperationsService.UpdateAsync(id: command.KidAccountOperationId,
            updateEntityFunction: dbEntity =>
            {
                dbEntity.Amount = command.Amount;
                dbEntity.Description = command.Title;
                dbEntity.DueDate = command.DueDate;
                dbEntity.OperationType = command.OperationType;
                dbEntity.UpdatedAt = clock.UtcNow;

                return dbEntity;
            }, cancellationToken: cancellationToken);
    }
}