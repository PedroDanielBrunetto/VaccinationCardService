namespace VaccinationCard.Application.Vaccinations.Queries.GetVaccinationCard
{
    public class VaccinationItemDto
    {
        public string VaccineName { get; init; } = null!;
        public int Dose { get; init; }
        public DateTime AppliedAt { get; init; }
    }
}
