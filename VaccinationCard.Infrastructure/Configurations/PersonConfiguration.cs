using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VaccinationCard.Domain.Entities;

namespace VaccinationCard.Infrastructure.Configurations
{
    public class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(p => p.Document)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasIndex(p => p.Document)
                .IsUnique();

            builder.HasMany(p => p.Vaccinations)
                .WithOne(v => v.Person)
                .HasForeignKey(v => v.PersonId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

}
