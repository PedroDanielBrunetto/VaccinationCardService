namespace VaccinationCard.Application.Vaccinations.Queries.GetVaccinationCard
{
    public class VaccinationCardDto
    {
        public Guid PersonId { get; init; }
        public string PersonName { get; init; } = null!;
        public string Document { get; init; } = null!;
        public IReadOnlyCollection<VaccinationItemDto> Vaccinations { get; init; }
            = Array.Empty<VaccinationItemDto>();
    }
}
