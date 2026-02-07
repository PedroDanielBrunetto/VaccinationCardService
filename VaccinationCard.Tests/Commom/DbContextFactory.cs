using Microsoft.EntityFrameworkCore;

namespace VaccinationCard.Tests.Common;

public static class DbContextFactory
{
    public static VaccinationDbContext Create()
    {
        var options = new DbContextOptionsBuilder<VaccinationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        return new VaccinationDbContext(options);
    }
}
