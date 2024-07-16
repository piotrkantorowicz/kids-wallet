using KidsWallet.Persistence.Model.Operations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KidsWallet.Persistence.Configurations;

public sealed class KidAccountOperationEntityTypeConfiguration : IEntityTypeConfiguration<KidAccountOperation>
{
    public void Configure(EntityTypeBuilder<KidAccountOperation> builder)
    {
        builder.ToTable(name: "kid_account_operations");
        builder.HasKey(keyExpression: e => e.Id);
        builder.Property(propertyExpression: e => e.Id).HasColumnName(name: "id").IsRequired().ValueGeneratedOnAdd();
        builder.Property(propertyExpression: e => e.KidAccountId).HasColumnName(name: "kid_account_id").IsRequired();
        builder.Property(propertyExpression: e => e.OperationType).HasColumnName(name: "operation_type").IsRequired();
        builder.Property(propertyExpression: e => e.DueDate).HasColumnName(name: "due_date").IsRequired();
        builder.Property(propertyExpression: e => e.Amount).HasColumnName(name: "amount").IsRequired();

        builder
            .Property(propertyExpression: e => e.Description)
            .HasColumnName(name: "description")
            .IsRequired()
            .HasMaxLength(maxLength: 200);

        builder.Property(propertyExpression: e => e.CreatedAt).HasColumnName(name: "created_at").IsRequired();

        builder
            .Property(propertyExpression: e => e.UpdatedAt)
            .HasColumnName(name: "updated_at")
            .IsRequired(required: false);
    }
}