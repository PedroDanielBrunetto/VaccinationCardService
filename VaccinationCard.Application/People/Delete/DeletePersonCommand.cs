using MediatR;

namespace VaccinationCard.Application.People.Delete
{
    public class DeletePersonCommand : IRequest
    {
        public Guid PersonId { get; }

        public DeletePersonCommand(Guid personId)
        {
            PersonId = personId;
        }
    }
}
