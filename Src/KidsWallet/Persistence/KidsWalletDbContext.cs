using KidsWallet.Persistence.Model.Accounts;
using KidsWallet.Persistence.Model.Operations;
using KidsWallet.Persistence.Model.Wallets;

using Microsoft.EntityFrameworkCore;

namespace KidsWallet.Persistence;

public sealed class KidsWalletDbContext(DbContextOptions<KidsWalletDbContext> options) : DbContext(options)
{
    public DbSet<KidWallet> KidWallets { get; init; } = null!;
    
    public DbSet<KidAccount> KidAccounts { get; init; } = null!;
    
    public DbSet<KidAccountOperation> Operations { get; init; } = null!;
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("kids_wallet");
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}