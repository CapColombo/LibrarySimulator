using FluentValidation;
using Library.DAL.Models.Employees;

namespace LibrarySimulator.Validators;

public class RoleValidator : AbstractValidator<Role>
{
    public RoleValidator()
    {
        RuleFor(r => r.Name).NotEmpty();
    }
}
