using MediatR;
using Microsoft.EntityFrameworkCore;
using VaccinationCard.Application.Commom.Exceptions;
using VaccinationCard.Application.Abstractions.Persistence;
using VaccinationCard.Domain.Entities;

namespace VaccinationCard.Application.People.Create
{
    public class CreatePersonCommandHandler : IRequestHandler<CreatePersonCommand, Guid>
    {
        private readonly IVaccinationDbContext _context;

        public CreatePersonCommandHandler(IVaccinationDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
        {
            var documentExists = await _context.Persons.AnyAsync(p => p.Document == request.Document, cancellationToken);

            if (documentExists)
                throw new ConflictException("O documento informado já possui cadastro.");

            var person = new Person(request.Name, request.Document, request.Gender, request.Age, request.Email);

            _context.Persons.Add(person);
            await _context.SaveChangesAsync(cancellationToken);

            return person.Id;
        }
    }
}
