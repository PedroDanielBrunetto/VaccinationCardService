namespace VaccinationCard.Domain.Entities
{
    public class Person
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public string Name { get; private set; }
        public string Document { get; private set; }

        public ICollection<Vaccination> Vaccinations { get; private set; } = new List<Vaccination>();

        protected Person() { }

        public Person(string name, string document)
        {
            Name = name;
            Document = document;
        }
    }

}
