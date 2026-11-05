using banking_api.Endpoints.DataTransferObjects;
using FluentValidation;

namespace banking_api.Validators
{
    public class OrganisationValidator : AbstractValidator<CreateOrganisationDto>
    {
        public OrganisationValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Organisation name is required.")
                .MaximumLength(100).WithMessage("Organisation name cannot exceed 100 characters.");

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Organisation address is required.")
                .MaximumLength(200).WithMessage("Organisation address cannot exceed 200 characters.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Organisation email is required.")
                .EmailAddress().WithMessage("Organisation email is not valid.");
        }
    }
}
