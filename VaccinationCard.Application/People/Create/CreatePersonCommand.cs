using MediatR;
using VaccinationCard.Domain.Enums;

namespace VaccinationCard.Application.People.Create
{
    public class CreatePersonCommand : IRequest<Guid>
    {
        public string Name { get; init; } = null!;
        public string Document { get; init; } = null!;
        public Gender Gender { get; init; }
        public int Age { get; init; }
        public string Email { get; init; } = null!;
    }
}
