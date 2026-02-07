using MediatR;

namespace VaccinationCard.Application.Vaccinations.Queries
{
    public class GetVaccinationCardQuery : IRequest<VaccinationCardDto>
    {
        public Guid PersonId { get; }

        public GetVaccinationCardQuery(Guid personId)
        {
            PersonId = personId;
        }
    }
}
