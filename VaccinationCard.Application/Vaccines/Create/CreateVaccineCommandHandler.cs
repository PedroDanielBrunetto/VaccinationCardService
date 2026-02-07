using MediatR;
using Microsoft.EntityFrameworkCore;
using VaccinationCard.Application.Abstractions.Persistence;
using VaccinationCard.Domain.Entities;

namespace VaccinationCard.Application.Vaccines.Create
{
    public class CreateVaccineCommandHandler : IRequestHandler<CreateVaccineCommand, Guid>
    {
        private readonly IVaccinationDbContext _context;

        public CreateVaccineCommandHandler(IVaccinationDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(CreateVaccineCommand request, CancellationToken cancellationToken)
        {
            var exists = await _context.Vaccines
                .AnyAsync(v => v.Name == request.Name, cancellationToken);

            if (exists)
                throw new InvalidOperationException("A vacina informada já possui cadastro.");

            var vaccine = new Vaccine(request.Name);

            _context.Vaccines.Add(vaccine);
            await _context.SaveChangesAsync(cancellationToken);

            return vaccine.Id;
        }
    }
}
