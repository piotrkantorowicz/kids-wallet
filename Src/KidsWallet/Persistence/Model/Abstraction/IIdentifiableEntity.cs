namespace KidsWallet.Persistence.Model.Abstraction;

public interface IIdentifiableEntity<out T> : IEntity
{
    T Id { get; }
}