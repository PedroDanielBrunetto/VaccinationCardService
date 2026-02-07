using MediatR;
using Microsoft.EntityFrameworkCore;
using VaccinationCard.Application.Abstractions.Persistence;

namespace VaccinationCard.Application.Vaccinations.Queries.GetVaccinationCard
{
    public class GetVaccinationCardQueryHandler : IRequestHandler<GetVaccinationCardQuery, VaccinationCardDto>
    {
        private readonly IVaccinationDbContext _context;

        public GetVaccinationCardQueryHandler(IVaccinationDbContext context)
        {
            _context = context;
        }

        public async Task<VaccinationCardDto> Handle(GetVaccinationCardQuery request, CancellationToken cancellationToken)
        {
            var person = await _context.Persons
                .AsNoTracking()
                .FirstOrDefaultAsync(
                    p => p.Id == request.PersonId,
                    cancellationToken);

            if (person is null)
                throw new InvalidOperationException("Pessoa não encontrada.");

            var vaccinations = await _context.Vaccinations
                .AsNoTracking()
                .Where(v => v.PersonId == request.PersonId)
                .Include(v => v.Vaccine)
                .OrderBy(v => v.AppliedAt)
                .Select(v => new VaccinationItemDto
                {
                    VaccineName = v.Vaccine.Name,
                    Dose = v.Dose,
                    AppliedAt = v.AppliedAt
                })
                .ToListAsync(cancellationToken);

            return new VaccinationCardDto
            {
                PersonId = person.Id,
                PersonName = person.Name,
                Document = person.Document,
                Vaccinations = vaccinations
            };
        }
    }
}
