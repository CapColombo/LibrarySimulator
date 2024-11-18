using FluentValidation;
using Library.DAL.Dto.Controllers;

namespace LibrarySimulator.Validators;

public class VisitorValidator : AbstractValidator<VisitorDto>
{
    public VisitorValidator()
    {
        RuleFor(v => v.Name).NotEmpty().MinimumLength(2);

        RuleFor(v => v.Email).EmailAddress();
    }
}
