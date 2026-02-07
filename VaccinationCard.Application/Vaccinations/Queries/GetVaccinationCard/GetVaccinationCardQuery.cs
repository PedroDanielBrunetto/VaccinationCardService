using MediatR;

namespace VaccinationCard.Application.Vaccinations.Queries
{
    public class GetVaccinationCardQuery : IRequest<VaccinationCardDTO>
    {
        public Guid PersonId { get; }

        public GetVaccinationCardQuery(Guid personId)
        {
            PersonId = personId;
        }
    }
}
