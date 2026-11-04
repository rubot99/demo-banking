using banking_api.Models;
using Microsoft.EntityFrameworkCore;

namespace banking_api.Data
{
    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Account> builder)
        {
            builder.ToTable("Accounts", "banking");

            builder.HasKey(o => o.Id);

            builder.Property(o => o.AccountNumber)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(o => o.AccountHolderAddress)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(o => o.AccountHolderName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(o => o.CreatedAt)
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(o => o.UpdatedAt)
                .IsRequired()
                .ValueGeneratedOnUpdate();

            builder.HasOne(p => p.Programme)
                .WithMany(a => a.Accounts)
                .HasForeignKey(p => p.ProgrammeId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
