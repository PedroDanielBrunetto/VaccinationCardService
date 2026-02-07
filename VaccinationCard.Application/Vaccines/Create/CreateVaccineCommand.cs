using MediatR;

namespace VaccinationCard.Application.Vaccines.Create
{
    public class CreateVaccineCommand : IRequest<Guid>
    {
        public string Name { get; init; } = null!;
    }
}
