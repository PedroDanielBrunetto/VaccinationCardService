namespace VaccinationCard.Application.Vaccinations.IntegrationEvents
{
    public class VaccinationCreatedIntegrationEvent
    {
        public Guid VaccinationId { get; set; }
        public Guid PersonId { get; set; }
        public Guid VaccineId { get; set; }
        public int Dose { get; set; }
        public DateTime OccurredAt { get; set; }

        public VaccinationCreatedIntegrationEvent(Guid vaccinationId, Guid personId, Guid vaccineId, int dose, DateTime occurredAt)
        {
            VaccinationId = vaccinationId;
            PersonId = personId;
            VaccineId = vaccineId;
            Dose = dose;
            OccurredAt = occurredAt;
        }
    }
}
