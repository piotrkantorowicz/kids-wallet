using KidsWallet.Persistence.Model.Wallets;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KidsWallet.Persistence.Configurations;

public sealed class KidWalletEntityTypeConfiguration : IEntityTypeConfiguration<KidWallet>
{
    public void Configure(EntityTypeBuilder<KidWallet> builder)
    {
        builder.ToTable("kid_wallets");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnName("id").IsRequired().ValueGeneratedOnAdd();
        builder.Property(x => x.Name).HasColumnName("name").IsRequired().HasMaxLength(100);
        builder.HasMany(x => x.KidAccounts).WithOne(x => x.KidWallet).HasForeignKey(x => x.KidWalletId);
        builder.Property(e => e.CreatedAt).HasColumnName("created_at").IsRequired();
        builder.Property(e => e.UpdatedAt).HasColumnName("updated_at").IsRequired(false);
    }
}