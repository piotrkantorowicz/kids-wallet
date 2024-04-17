using KidsWallet.Persistence.Model.Abstraction;
using KidsWallet.Persistence.Model.Operations;
using KidsWallet.Persistence.Model.Wallets;

namespace KidsWallet.Persistence.Model.Accounts;

public sealed class KidAccount : IAuditableEntity<Guid>
{
    public Guid KidWalletId { get; set; }
    
    public string? Name { get; set; }
    
    public decimal Balance { get; set; }
    
    public KidWallet? KidWallet { get; set; }
    
    public HashSet<KidAccountOperation> KidAccountOperations { get; set; } = [];
    
    public Guid Id { get; set; }
    
    public DateTimeOffset CreatedAt { get; set; }
    
    public DateTimeOffset? UpdatedAt { get; set; }
}