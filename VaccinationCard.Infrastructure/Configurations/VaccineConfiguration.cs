using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VaccinationCard.Domain.Entities;

namespace VaccinationCard.Infrastructure.Configurations
{
    public class VaccineConfiguration : IEntityTypeConfiguration<Vaccine>
    {
        public void Configure(EntityTypeBuilder<Vaccine> builder)
        {
            builder.HasKey(v => v.Id);

            builder.Property(v => v.Name)
                .IsRequired()
                .HasMaxLength(150);

            builder.HasIndex(v => v.Name)
                .IsUnique();
        }
    }

}
