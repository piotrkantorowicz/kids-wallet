using KidsWallet.Persistence.Model.Abstraction;
using KidsWallet.Persistence.Model.Accounts;

namespace KidsWallet.Persistence.Model.Operations;

public sealed class KidAccountOperation : IAuditableEntity<Guid>
{
    public Guid KidAccountId { get; set; }
    
    public KidAccount? KidAccount { get; set; }
    
    public decimal Amount { get; set; }
    
    public string? Description { get; set; }
    
    public DateTime DueDate { get; set; }
    
    public OperationType OperationType { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public DateTime? UpdatedAt { get; set; }
    
    public Guid Id { get; set; }
}