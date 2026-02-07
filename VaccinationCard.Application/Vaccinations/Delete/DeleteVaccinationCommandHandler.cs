using MediatR;
using Microsoft.EntityFrameworkCore;
using VaccinationCard.Application.Abstractions.Persistence;
using VaccinationCard.Application.Commom.Exceptions;

namespace VaccinationCard.Application.Vaccinations.Delete
{
    public class DeleteVaccinationCommandHandler : IRequestHandler<DeleteVaccinationCommand>
    {
        private readonly IVaccinationDbContext _context;

        public DeleteVaccinationCommandHandler(IVaccinationDbContext context)
        {
            _context = context;
        }

        public async Task Handle(DeleteVaccinationCommand request, CancellationToken cancellationToken)
        {
            var vaccination = await _context.Vaccinations.FirstOrDefaultAsync(v => v.Id == request.Id, cancellationToken);

            if (vaccination is null)
                throw new NotFoundException("Vacinação não encontrada.");

            _context.Vaccinations.Remove(vaccination);

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
