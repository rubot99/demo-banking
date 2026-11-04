using banking_api.Models;
using Microsoft.EntityFrameworkCore;

namespace banking_api.Data
{
    public class ProgrammeConfiguration : IEntityTypeConfiguration<Programme>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Programme> builder)
        {
            builder.ToTable("Programmes", "banking");

            builder.HasKey(o => o.Id);

            builder.Property(o => o.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(o => o.Description)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(o => o.StartDate)
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(o => o.EndDate)
                .IsRequired()
                .ValueGeneratedOnUpdate();

            builder.Property(o => o.StartRange)
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(o => o.EndRange)
                .IsRequired()
                .ValueGeneratedOnUpdate();

            builder.Property(o => o.CreatedAt)
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(o => o.UpdatedAt)
                .IsRequired()
                .ValueGeneratedOnUpdate();

            builder.HasOne(p => p.Organisation)
                .WithMany(o => o.Programmes)
                .HasForeignKey(p => p.OrganisationId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
