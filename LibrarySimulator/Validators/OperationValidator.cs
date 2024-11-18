using FluentValidation;
using Library.DAL.Dto.Controllers;

namespace LibrarySimulator.Validators;

public class OperationValidator : AbstractValidator<OperationDto>
{
    public OperationValidator()
    {
        RuleFor(o => o.BookId).NotEmpty();

        RuleFor(o => o.VisitorId).NotEmpty();

        RuleFor(o => o.OperationType).IsInEnum();

        RuleFor(o => o.PhysicalCondition).IsInEnum();
    }
}
