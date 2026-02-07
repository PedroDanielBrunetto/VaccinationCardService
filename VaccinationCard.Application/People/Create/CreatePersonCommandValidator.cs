using FluentValidation;

namespace VaccinationCard.Application.People.Create
{
    public class CreatePersonCommandValidator : AbstractValidator<CreatePersonCommand>
    {
        public CreatePersonCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(200);

            RuleFor(x => x.Document)
                .NotEmpty()
                .Length(11)
                .Matches(@"^\d+$");

            RuleFor(x => x.Age)
                .NotNull();

            RuleFor(x => x.Gender)
                .NotNull()
                .IsInEnum();

            RuleFor(x => x.Email)
                .NotNull()
                .EmailAddress();
        }
    }
}
