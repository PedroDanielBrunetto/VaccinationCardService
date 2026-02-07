using VaccinationCard.Domain.Enums;

namespace VaccinationCard.Application.People.Queries.GetAllPersons
{
    public class PersonDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Document { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
    }
}
