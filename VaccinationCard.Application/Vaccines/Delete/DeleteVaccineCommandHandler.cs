using MediatR;
using Microsoft.EntityFrameworkCore;
using VaccinationCard.Application.Abstractions.Persistence;
using VaccinationCard.Application.Commom.Exceptions;

namespace VaccinationCard.Application.Vaccines.Delete
{
    public class DeleteVaccineCommandHandler : IRequestHandler<DeleteVaccineCommand>
    {
        private readonly IVaccinationDbContext _context;

        public DeleteVaccineCommandHandler(IVaccinationDbContext context)
        {
            _context = context;
        }

        public async Task Handle(DeleteVaccineCommand request, CancellationToken cancellationToken)
        {
            var vaccine = await _context.Vaccines.FirstOrDefaultAsync(v => v.Id == request.Id, cancellationToken);

            if (vaccine is null)
                throw new NotFoundException("Vacina não encontrada.");

            _context.Vaccines.Remove(vaccine);

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
