using MediatR;

namespace VaccinationCard.Application.Vaccinations.Delete
{
    public class DeleteVaccinationCommand : IRequest
    {
        public Guid Id { get; set; }
        public DeleteVaccinationCommand(Guid id)
        {
            Id = id;
        }
    }
}
