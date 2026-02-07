namespace VaccinationCard.Domain.Entities
{
    public class Vaccine
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public string Name { get; private set; }

        protected Vaccine() { }

        public Vaccine(string name)
        {
            Name = name;
        }
    }

}
