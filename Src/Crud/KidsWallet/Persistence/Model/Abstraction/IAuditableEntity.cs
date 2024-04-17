namespace KidsWallet.Persistence.Model.Abstraction;

public interface IAuditableEntity<out T> : IIdentifiableEntity<T>
{
    DateTimeOffset CreatedAt { get; set; }
    
    DateTimeOffset? UpdatedAt { get; set; }
}