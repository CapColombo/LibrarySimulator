using FluentValidation;
using LibrarySimulator.Controllers.Dto;

namespace LibrarySimulator.Validators;

public class VisitorValidator : AbstractValidator<VisitorDto>
{
    public VisitorValidator()
    {
        RuleFor(v => v.Name).NotEmpty().MinimumLength(2);

        RuleFor(v => v.Email).EmailAddress();
    }
}
