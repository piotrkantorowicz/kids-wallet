using KidsWallet.Persistence.Model.Abstraction;
using KidsWallet.Persistence.Model.Accounts;

namespace KidsWallet.Persistence.Model.Wallets;

public sealed class KidWallet : IAuditableEntity<Guid>
{
    public Guid KidId { get; set; }
    
    public string? Name { get; set; }
    
    public HashSet<KidAccount> KidAccounts { get; set; } = [];
    
    public Guid Id { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public DateTime? UpdatedAt { get; set; }
}