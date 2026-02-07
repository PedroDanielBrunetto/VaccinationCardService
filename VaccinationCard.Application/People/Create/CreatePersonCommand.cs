using MediatR;
using VaccinationCard.Domain.Enums;

namespace VaccinationCard.Application.People.Create
{
    public class CreatePersonCommand : IRequest<Guid>
    {
        public string Name { get; init; } = null!;
        public string Document { get; init; } = null!;
        public Gender Gender { get; init; }
        public DateTime Birth { get; init; }
        public string Email { get; init; } = null!;

        public CreatePersonCommand(string name, string document, Gender gender, DateTime birth, string email)
        {
            Name = name;
            Document = document;
            Gender = gender;
            Birth = birth;
            Email = email;
        }
    }
}
