using MediatR;

namespace VaccinationCard.Application.People.Create
{
    public class CreatePersonCommand : IRequest<Guid>
    {
        public string Name { get; init; } = null!;
        public string Document { get; init; } = null!;
    }
}
