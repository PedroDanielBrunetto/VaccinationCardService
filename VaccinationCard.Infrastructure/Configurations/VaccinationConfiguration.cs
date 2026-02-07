using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VaccinationCard.Domain.Entities;

namespace VaccinationCard.Infrastructure.Configurations
{
    public class VaccinationConfiguration : IEntityTypeConfiguration<Vaccination>
    {
        public void Configure(EntityTypeBuilder<Vaccination> builder)
        {
            builder.HasKey(v => v.Id);

            builder.Property(v => v.Dose)
                .IsRequired();

            builder.Property(v => v.AppliedAt)
                .IsRequired();

            builder.HasOne(v => v.Person)
                .WithMany(p => p.Vaccinations)
                .HasForeignKey(v => v.PersonId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(v => v.Vaccine)
                .WithMany()
                .HasForeignKey(v => v.VaccineId);
        }
    }

}
