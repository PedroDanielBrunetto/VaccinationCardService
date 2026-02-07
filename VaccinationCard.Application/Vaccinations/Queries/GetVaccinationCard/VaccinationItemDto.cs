namespace VaccinationCard.Application.Vaccinations.Queries
{
    public class VaccinationItemDTO
    {
        public string VaccineName { get; init; } = null!;
        public int Dose { get; init; }
        public DateTime AppliedAt { get; init; }
    }
}
