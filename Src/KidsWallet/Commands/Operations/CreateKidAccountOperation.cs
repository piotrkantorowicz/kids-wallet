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
        RuleFor(expression: x => x.KidAccountOperationId).NotEmpty();
        RuleFor(expression: x => x.KidAccountId).NotEmpty();
        RuleFor(expression: x => x.Amount);
        RuleFor(expression: x => x.Title).NotEmpty().Length(min: 3, max: 200);
        RuleFor(expression: x => x.DueDate).NotEmpty();
        RuleFor(expression: x => x.OperationType).NotEmpty();
    }
}

public static class CreateKidAccountOperationCommandHandler
{
    [Transactional]
    public static async Task Handle(CreateKidAccountOperationCommand command,
        ICrudOperationsService<KidAccountOperation> kidAccountOperationCrudOperationsService,
        ICrudOperationsService<KidAccount> kidAccountCrudOperationsService, CancellationToken cancellationToken)
    {
        KidAccount? kidAccount = await kidAccountCrudOperationsService.GetByIdAsync(id: command.KidAccountId,
            throwWhenNotFound: true, cancellationToken: cancellationToken);

        await kidAccountOperationCrudOperationsService.CreateAsync(id: command.KidAccountId, createEntityFunc: () =>
            new KidAccountOperation
            {
                Id = command.KidAccountOperationId,
                KidAccountId = command.KidAccountId,
                KidAccount = kidAccount,
                Amount = command.Amount,
                Description = command.Title,
                DueDate = command.DueDate,
                CreatedAt = DateTime.UtcNow,
                OperationType = command.OperationType
            }, cancellationToken: cancellationToken);
    }
}