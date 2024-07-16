using KidsWallet.Persistence.Model.Accounts;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KidsWallet.Persistence.Configurations;

public sealed class KidAccountEntityTypeConfiguration : IEntityTypeConfiguration<KidAccount>
{
    public void Configure(EntityTypeBuilder<KidAccount> builder)
    {
        builder.ToTable(name: "kid_accounts");
        builder.HasKey(keyExpression: x => x.Id);
        builder.Property(propertyExpression: x => x.Id).HasColumnName(name: "id").IsRequired().ValueGeneratedOnAdd();

        builder
            .Property(propertyExpression: x => x.Name)
            .HasColumnName(name: "name")
            .IsRequired()
            .HasMaxLength(maxLength: 100);

        builder.Property(propertyExpression: x => x.KidWalletId).HasColumnName(name: "kid_wallet_id").IsRequired();
        builder.Property(propertyExpression: x => x.Balance).HasColumnName(name: "balance").IsRequired();

        builder
            .HasMany(navigationExpression: x => x.KidAccountOperations)
            .WithOne(navigationExpression: x => x.KidAccount)
            .HasForeignKey(foreignKeyExpression: x => x.KidAccountId);

        builder.Property(propertyExpression: e => e.CreatedAt).HasColumnName(name: "created_at").IsRequired();

        builder
            .Property(propertyExpression: e => e.UpdatedAt)
            .HasColumnName(name: "updated_at")
            .IsRequired(required: false);
    }
}