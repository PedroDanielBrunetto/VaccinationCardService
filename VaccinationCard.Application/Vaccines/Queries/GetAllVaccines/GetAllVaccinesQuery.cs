using MediatR;

namespace VaccinationCard.Application.Vaccines.Queries
{
    public class GetAllVaccinesQuery : IRequest<List<VaccineDTO>>
    {
    }
}
