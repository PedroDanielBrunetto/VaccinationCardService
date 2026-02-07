using MediatR;
using Microsoft.EntityFrameworkCore;
using VaccinationCard.Application.Abstractions.Persistence;

namespace VaccinationCard.Application.Vaccines.Queries
{
    public class GetAllVaccinesQueryHandler : IRequestHandler<GetAllVaccinesQuery, List<VaccineDTO>>
    {
        private readonly IVaccinationDbContext _context;

        public GetAllVaccinesQueryHandler(IVaccinationDbContext context)
        {
            _context = context;
        }

        public async Task<List<VaccineDTO>> Handle(GetAllVaccinesQuery request, CancellationToken cancellationToken)
        {
            var persons = await _context.Vaccines
                .AsNoTracking()
                .Select(p => new VaccineDTO
                {
                    Id = p.Id,
                    Name = p.Name
                })
                .ToListAsync(cancellationToken);

            return persons;
        }
    }
}
