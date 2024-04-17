using KidsWallet.Persistence.Model.Accounts;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KidsWallet.Persistence.Configurations;

public sealed class KidAccountEntityTypeConfiguration : IEntityTypeConfiguration<KidAccount>
{
    public void Configure(EntityTypeBuilder<KidAccount> builder)
    {
        builder.ToTable("kid_accounts");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnName("id").IsRequired().ValueGeneratedOnAdd();
        builder.Property(x => x.Name).HasColumnName("name").IsRequired().HasMaxLength(100);
        builder.Property(x => x.KidWalletId).HasColumnName("kid_wallet_id").IsRequired();
        builder.Property(x => x.Balance).HasColumnName("balance").IsRequired();
        builder.HasMany(x => x.KidAccountOperations).WithOne(x => x.KidAccount).HasForeignKey(x => x.KidAccountId);
        builder.Property(e => e.CreatedAt).HasColumnName("created_at").IsRequired();
        builder.Property(e => e.UpdatedAt).HasColumnName("updated_at").IsRequired(false);
    }
}