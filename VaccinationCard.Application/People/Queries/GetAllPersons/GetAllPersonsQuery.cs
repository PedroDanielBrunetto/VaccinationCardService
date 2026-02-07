using MediatR;

namespace VaccinationCard.Application.People.Queries.GetAllPersons
{
    public class GetAllPersonsQuery : IRequest<List<PersonDTO>>
    {
    }
}
