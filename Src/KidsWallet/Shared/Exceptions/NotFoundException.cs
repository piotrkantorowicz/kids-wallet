using KidsWallet.Persistence.Model.Abstraction;

namespace KidsWallet.Shared.Exceptions;

public sealed class NotFoundException : Exception
{
    public NotFoundException(object? id) : base(message: $"Entity with id {id} not found.")
    {
    }
}

public sealed class NotFoundException<T> : Exception where T : class, IAuditableEntity<Guid>
{
    public NotFoundException(ISpecification<T>? specification) : base(
        message: $"Entity with specification {specification} not found.")
    {
    }
}