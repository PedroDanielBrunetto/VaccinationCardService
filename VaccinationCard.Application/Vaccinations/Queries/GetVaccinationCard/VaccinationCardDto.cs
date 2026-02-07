namespace VaccinationCard.Application.Vaccinations.Queries
{
    public class VaccinationCardDTO
    {
        public Guid PersonId { get; init; }
        public string PersonName { get; init; } = null!;
        public string Document { get; init; } = null!;
        public DateTime Birth { get; init; }
        public string Email { get; init; } = null!;
        public string Gender { get; init; } = null!;
        public IReadOnlyCollection<VaccinationItemDTO> Vaccinations { get; init; }
            = Array.Empty<VaccinationItemDTO>();
    }
}
