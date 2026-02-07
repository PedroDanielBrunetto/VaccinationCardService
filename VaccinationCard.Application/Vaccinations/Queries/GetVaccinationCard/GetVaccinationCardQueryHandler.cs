using MediatR;
using Microsoft.EntityFrameworkCore;
using VaccinationCard.Application.Abstractions.Persistence;
using VaccinationCard.Application.Commom.Exceptions;

namespace VaccinationCard.Application.Vaccinations.Queries
{
    public class GetVaccinationCardQueryHandler : IRequestHandler<GetVaccinationCardQuery, VaccinationCardDTO>
    {
        private readonly IVaccinationDbContext _context;

        public GetVaccinationCardQueryHandler(IVaccinationDbContext context)
        {
            _context = context;
        }

        public async Task<VaccinationCardDTO> Handle(GetVaccinationCardQuery request, CancellationToken cancellationToken)
        {
            var person = await _context.Persons
                .AsNoTracking()
                .FirstOrDefaultAsync(
                    p => p.Id == request.PersonId,
                    cancellationToken);

            if (person is null)
                throw new NotFoundException("Pessoa não encontrada.");

            var vaccinations = await _context.Vaccinations
                .AsNoTracking()
                .Where(v => v.PersonId == request.PersonId)
                .Include(v => v.Vaccine)
                .OrderBy(v => v.AppliedAt)
                .Select(v => new VaccinationItemDTO
                {
                    VaccineName = v.Vaccine.Name,
                    Dose = v.Dose,
                    AppliedAt = v.AppliedAt
                })
                .ToListAsync(cancellationToken);

            return new VaccinationCardDTO
            {
                PersonId = person.Id,
                PersonName = person.Name,
                Document = person.Document,
                Gender = person.Gender.ToString(),
                Birth = person.Birth,
                Email = person.Email,
                Vaccinations = vaccinations
            };
        }
    }
}
