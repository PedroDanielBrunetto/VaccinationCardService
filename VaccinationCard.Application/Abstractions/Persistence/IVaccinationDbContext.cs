using Microsoft.EntityFrameworkCore;
using VaccinationCard.Domain.Entities;

namespace VaccinationCard.Application.Abstractions.Persistence
{
    public interface IVaccinationDbContext
    {
        DbSet<Person> Persons { get; }
        DbSet<Vaccine> Vaccines { get; }
        DbSet<Vaccination> Vaccinations { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
