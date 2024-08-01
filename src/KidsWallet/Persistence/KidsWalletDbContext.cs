using KidsWallet.Persistence.Model.Accounts;
using KidsWallet.Persistence.Model.Operations;
using KidsWallet.Persistence.Model.Wallets;

using Microsoft.EntityFrameworkCore;

namespace KidsWallet.Persistence;

public sealed class KidsWalletDbContext : DbContext
{
    public KidsWalletDbContext(DbContextOptions<KidsWalletDbContext> options) : base(options: options)
    {
    }

    public DbSet<KidWallet> KidWallets { get; init; } = null!;

    public DbSet<KidAccount> KidAccounts { get; init; } = null!;

    public DbSet<KidAccountOperation> Operations { get; init; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(schema: "kids_wallet");
        modelBuilder.ApplyConfigurationsFromAssembly(assembly: GetType().Assembly);
    }
}