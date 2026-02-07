using MediatR;

namespace VaccinationCard.Application.People.Queries
{
    public class GetAllPersonsQuery : IRequest<List<PersonDTO>>
    {
    }
}
