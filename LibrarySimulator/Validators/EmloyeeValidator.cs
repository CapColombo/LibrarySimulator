using FluentValidation;
using Library.DAL.Dto.Controllers;

namespace LibrarySimulator.Validators;

public class EmloyeeValidator : AbstractValidator<EmployeeDto>
{
    public EmloyeeValidator()
    {
        RuleFor(e => e.Name).NotEmpty().MinimumLength(2);

        RuleFor(e => e.Role).SetValidator(new RoleValidator());

        RuleFor(e => e.WorkSchedule).IsInEnum();
    }
}
