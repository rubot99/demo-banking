using banking_api.Models;
using Microsoft.EntityFrameworkCore;

namespace banking_api.Data
{
    public class OrganisationConfiguration : IEntityTypeConfiguration<Organisation>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Organisation> builder)
        {
            builder.ToTable("Organisations", "banking");

            builder.HasKey(o => o.Id);

            builder.Property(o => o.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(o => o.Address)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(o => o.Email)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(o => o.CreatedAt)
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(o => o.UpdatedAt)
                .IsRequired()
                .ValueGeneratedOnUpdate();
        }
    }
}
