using MediatR;
using Microsoft.EntityFrameworkCore;
using VaccinationCard.Application.Abstractions.Persistence;

namespace VaccinationCard.Application.People.Queries.GetAllPersons
{
    public class GetAllPersonsQueryHandler : IRequestHandler<GetAllPersonsQuery, List<PersonDTO>>
    {
        private readonly IVaccinationDbContext _context;

        public GetAllPersonsQueryHandler(IVaccinationDbContext context)
        {
            _context = context;
        }

        public async Task<List<PersonDTO>> Handle(GetAllPersonsQuery request, CancellationToken cancellationToken)
        {
            var persons = await _context.Persons
                .AsNoTracking()
                .Select(p => new PersonDTO
                {
                    Id = p.Id,
                    Name = p.Name,
                    Document = p.Document,
                    Gender = p.Gender.ToString(),
                    Email = p.Email,
                    Age = p.Age
                })
                .ToListAsync(cancellationToken);

            return persons;
        }
    }
}
