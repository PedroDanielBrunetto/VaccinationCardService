using MediatR;
using Microsoft.EntityFrameworkCore;
using VaccinationCard.Application.Commom.Exceptions;
using VaccinationCard.Application.Abstractions.Persistence;

namespace VaccinationCard.Application.People.Delete
{
    public class DeletePersonCommandHandler : IRequestHandler<DeletePersonCommand>
    {
        private readonly IVaccinationDbContext _context;

        public DeletePersonCommandHandler(IVaccinationDbContext context)
        {
            _context = context;
        }

        public async Task Handle(DeletePersonCommand request, CancellationToken cancellationToken)
        {
            var person = await _context.Persons.FirstOrDefaultAsync(p => p.Id == request.PersonId, cancellationToken);

            if (person is null)
                throw new NotFoundException("Pessoa não encontrada.");

            _context.Persons.Remove(person);

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}