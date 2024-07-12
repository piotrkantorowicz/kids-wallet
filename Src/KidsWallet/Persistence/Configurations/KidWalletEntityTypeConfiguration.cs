using KidsWallet.Persistence.Model.Wallets;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KidsWallet.Persistence.Configurations;

public sealed class KidWalletEntityTypeConfiguration : IEntityTypeConfiguration<KidWallet>
{
    public void Configure(EntityTypeBuilder<KidWallet> builder)
    {
        builder.ToTable(name: "kid_wallets");
        builder.HasKey(keyExpression: x => x.Id);
        builder.Property(propertyExpression: x => x.Id).HasColumnName(name: "id").IsRequired().ValueGeneratedOnAdd();

        builder
            .Property(propertyExpression: x => x.Name)
            .HasColumnName(name: "name")
            .IsRequired()
            .HasMaxLength(maxLength: 100);

        builder
            .HasMany(navigationExpression: x => x.KidAccounts)
            .WithOne(navigationExpression: x => x.KidWallet)
            .HasForeignKey(foreignKeyExpression: x => x.KidWalletId);

        builder.Property(propertyExpression: e => e.CreatedAt).HasColumnName(name: "created_at").IsRequired();

        builder
            .Property(propertyExpression: e => e.UpdatedAt)
            .HasColumnName(name: "updated_at")
            .IsRequired(required: false);
    }
}