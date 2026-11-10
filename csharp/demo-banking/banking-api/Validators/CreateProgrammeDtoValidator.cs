using banking_api.Endpoints.DataTransferObjects;
using FluentValidation;

namespace banking_api.Validators
{
    public class CreateProgrammeDtoValidator : AbstractValidator<CreateProgrammeDto>
    {
        public CreateProgrammeDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Programme name is required.")
                .MaximumLength(100).WithMessage("Programme name cannot exceed 100 characters.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Programme description is required.")
                .MaximumLength(200).WithMessage("Programme description cannot exceed 200 characters.");
        }
    }
}
