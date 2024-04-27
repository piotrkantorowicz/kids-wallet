using FluentValidation;

using KidsWallet.Persistence.Model.Operations;
using KidsWallet.Services.Abstraction;
using KidsWallet.Shared.Abstraction;

using Wolverine.Attributes;

namespace KidsWallet.Commands.Operations;

public sealed record UpdateKidAccountOperationRequest(
    decimal Amount,
    string Title,
    DateTime DueDate,
    UpdateKidAccountOperationRequest_OperationType OperationType);

public enum UpdateKidAccountOperationRequest_OperationType
{
    None,
    Income,
    Expense,
    Transfer
}

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
        RuleFor(x => x.KidAccountOperationId).NotEmpty();
        RuleFor(x => x.Amount);
        RuleFor(x => x.Title).NotEmpty().Length(3, 200);
        RuleFor(x => x.DueDate).NotEmpty();
    }
}

public static class UpdateKidAccountOperationCommandHandler
{
    [Transactional]
    public static async Task Handle(UpdateKidAccountOperationCommand command,
        ICrudOperationsService<KidAccountOperation> kidOperationCrudOperationsService, IClock clock,
        CancellationToken cancellationToken)
    {
        await kidOperationCrudOperationsService.UpdateAsync(command.KidAccountOperationId, dbEntity =>
        {
            dbEntity.Amount = command.Amount;
            dbEntity.Description = command.Title;
            dbEntity.DueDate = command.DueDate;
            dbEntity.OperationType = command.OperationType;
            dbEntity.UpdatedAt = clock.UtcNow;
            
            return dbEntity;
        }, cancellationToken);
    }
}