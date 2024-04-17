using KidsWallet.Persistence.Model.Operations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KidsWallet.Persistence.Configurations;

public sealed class KidAccountOperationEntityTypeConfiguration : IEntityTypeConfiguration<KidAccountOperation>
{
    public void Configure(EntityTypeBuilder<KidAccountOperation> builder)
    {
        builder.ToTable("kid_account_operations");
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).HasColumnName("id").IsRequired().ValueGeneratedOnAdd();
        builder.Property(e => e.KidAccountId).HasColumnName("kid_account_id").IsRequired();
        builder.Property(e => e.OperationType).HasColumnName("operation_type").IsRequired();
        builder.Property(e => e.DueDate).HasColumnName("due_date").IsRequired();
        builder.Property(e => e.Amount).HasColumnName("amount").IsRequired();
        builder.Property(e => e.Description).HasColumnName("description").IsRequired().HasMaxLength(200);
        builder.Property(e => e.CreatedAt).HasColumnName("created_at").IsRequired();
        builder.Property(e => e.UpdatedAt).HasColumnName("updated_at").IsRequired(false);
    }
}