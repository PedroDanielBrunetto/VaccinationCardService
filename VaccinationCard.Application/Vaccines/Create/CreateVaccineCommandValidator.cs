using FluentValidation;

namespace VaccinationCard.Application.Vaccines.Create
{
    public class CreateVaccineCommandValidator : AbstractValidator<CreateVaccineCommand>
    {
        public CreateVaccineCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(150);
        }
    }
}
