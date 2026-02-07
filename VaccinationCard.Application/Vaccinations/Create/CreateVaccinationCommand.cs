using MediatR;

namespace VaccinationCard.Application.Vaccinations.Create
{
    public class CreateVaccinationCommand : IRequest<Guid>
    {
        public Guid PersonId { get; init; }
        public Guid VaccineId { get; init; }
        public int Dose { get; init; }
    }
}
