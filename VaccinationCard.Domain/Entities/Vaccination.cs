namespace VaccinationCard.Domain.Entities
{
    public class Vaccination
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public Guid PersonId { get; private set; }
        public Guid VaccineId { get; private set; }

        public int Dose { get; private set; }
        public DateTime AppliedAt { get; private set; }

        public Person Person { get; private set; }
        public Vaccine Vaccine { get; private set; }

        protected Vaccination() { }

        public Vaccination(Guid personId, Guid vaccineId, int dose)
        {
            PersonId = personId;
            VaccineId = vaccineId;
            Dose = dose;
            AppliedAt = DateTime.UtcNow;
        }
    }

}
