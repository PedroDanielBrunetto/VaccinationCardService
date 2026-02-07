using FluentValidation;

namespace VaccinationCard.Application.Vaccinations.Create
{
    public class CreateVaccinationCommandValidator : AbstractValidator<CreateVaccinationCommand>
    {
        public CreateVaccinationCommandValidator()
        {
            RuleFor(x => x.PersonId)
                .NotEmpty();

            RuleFor(x => x.VaccineId)
                .NotEmpty();

            RuleFor(x => x.Dose)
                .GreaterThan(0);
        }
    }
}
