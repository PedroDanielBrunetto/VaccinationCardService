using MediatR;

namespace VaccinationCard.Application.Vaccines.Delete
{
    public class DeleteVaccineCommand : IRequest
    {
        public Guid Id { get; set; }
        public DeleteVaccineCommand(Guid id)
        {
            Id = id;
        }
    }
}
