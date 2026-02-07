using MediatR;
using Microsoft.EntityFrameworkCore;
using VaccinationCard.Application.Abstractions.Persistence;
using VaccinationCard.Application.Commom.Exceptions;
using VaccinationCard.Domain.Entities;
using VaccinationCard.Domain.Events;

namespace VaccinationCard.Application.Vaccinations.Create
{
    public class CreateVaccinationCommandHandler : IRequestHandler<CreateVaccinationCommand, Guid>
    {
        private readonly IVaccinationDbContext _context;
        private readonly IMediator _mediator;

        public CreateVaccinationCommandHandler(IVaccinationDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<Guid> Handle(CreateVaccinationCommand request, CancellationToken cancellationToken)
        {
            var personExists = await _context.Persons.AnyAsync(p => p.Id == request.PersonId, cancellationToken);

            if (!personExists)
                throw new NotFoundException("Pessoa não encontrada.");

            var vaccineExists = await _context.Vaccines.AnyAsync(v => v.Id == request.VaccineId, cancellationToken);

            if (!vaccineExists)
                throw new NotFoundException("Vacina não encontrada.");

            var previousDoses = await _context.Vaccinations
                .Where(v =>
                    v.PersonId == request.PersonId &&
                    v.VaccineId == request.VaccineId)
                .Select(v => v.Dose)
                .ToListAsync(cancellationToken);

            if (previousDoses.Contains(request.Dose))
                throw new ConflictException("Essa dose já foi aplicada.");

            var expectedNextDose = previousDoses.Count == 0
                ? 1
                : previousDoses.Max() + 1;

            if (request.Dose != expectedNextDose)
                throw new ConflictException(
                    $"Sequência de dose inválida. A dose correta é {expectedNextDose}.");

            var vaccination = new Vaccination(
                request.PersonId,
                request.VaccineId,
                request.Dose);

            _context.Vaccinations.Add(vaccination);
            await _context.SaveChangesAsync(cancellationToken);

            await _mediator.Publish(new VaccinationCreatedEvent(
                vaccination.Id,
                vaccination.PersonId,
                vaccination.VaccineId,
                vaccination.Dose,
                DateTime.UtcNow
                ), cancellationToken);

            return vaccination.Id;
        }
    }
}
