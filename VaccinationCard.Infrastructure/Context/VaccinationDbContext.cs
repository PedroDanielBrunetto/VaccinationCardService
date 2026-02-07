using Microsoft.EntityFrameworkCore;
using VaccinationCard.Domain.Entities;

public class VaccinationDbContext : DbContext
{
    public VaccinationDbContext(DbContextOptions<VaccinationDbContext> options)
        : base(options) { }

    public DbSet<Person> Persons => Set<Person>();
    public DbSet<Vaccine> Vaccines => Set<Vaccine>();
    public DbSet<Vaccination> Vaccinations => Set<Vaccination>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(VaccinationDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
