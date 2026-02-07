namespace VaccinationCard.Application.People.Queries
{ 
    public class PersonDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Document { get; set; } = null!;
        public string Gender { get; set; } = null!;
        public DateTime Birth { get; set; }
        public string Email { get; set; } = null!;
    }
}
