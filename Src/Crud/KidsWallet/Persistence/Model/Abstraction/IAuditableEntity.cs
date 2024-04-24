namespace KidsWallet.Persistence.Model.Abstraction;

public interface IAuditableEntity<out T> : IIdentifiableEntity<T>
{
    DateTime CreatedAt { get; set; }
    
    DateTime? UpdatedAt { get; set; }
}