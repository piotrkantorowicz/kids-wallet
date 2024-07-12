using KidsWallet.Persistence.Model.Abstraction;

namespace KidsWallet.Shared.Exceptions;

public sealed class NotFoundException(object? id) : Exception(message: $"Entity with id {id} not found.");

public sealed class NotFoundException<T>(ISpecification<T>? specification)
    : Exception(message: $"Entity with specification {specification} not found.")
    where T : class, IAuditableEntity<Guid>;