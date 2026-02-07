using VaccinationCard.Domain.Enums;

namespace VaccinationCard.Domain.Entities
{
    public class Person
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public string Name { get; private set; }
        public string Document { get; private set; }
        public Gender Gender { get; private set; }
        public int Age { get; private set; }
        public string Email { get; private set; }

        public ICollection<Vaccination> Vaccinations { get; private set; } = new List<Vaccination>();

        protected Person() { }

        public Person(string name, string document, Gender gender, int age, string email)
        {
            Name = name;
            Document = document;
            Gender = gender;
            Age = age;
            Email = email;
        }
    }

}
